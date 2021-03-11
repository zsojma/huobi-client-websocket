using System;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages;
using Huobi.Client.Websocket.Messages.Pulling.Candlestick;
using Huobi.Client.Websocket.Messages.Pulling.Depth;
using Huobi.Client.Websocket.Messages.Subscription;
using Huobi.Client.Websocket.Messages.Subscription.Candlestick;
using Huobi.Client.Websocket.Messages.Subscription.Depth;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging;
using Websocket.Client;

namespace Huobi.Client.Websocket.Client
{
    internal class HuobiWebsocketClient : IHuobiWebsocketClient
    {
        private readonly IHuobiWebsocketCommunicator _communicator;
        private readonly IHuobiSerializer _serializer;
        private readonly ILogger<HuobiWebsocketClient> _logger;

        private readonly IDisposable _messageReceivedSubscription;

        public HuobiWebsocketClient(
            IHuobiWebsocketCommunicator communicator,
            IHuobiSerializer serializer,
            ILogger<HuobiWebsocketClient> logger)
        {
            _communicator = communicator;
            _serializer = serializer;
            _logger = logger;

            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);
        }

        public HuobiClientStreams Streams { get; } = new();

        public void Dispose()
        {
            _messageReceivedSubscription.Dispose();
        }

        public void Start()
        {
            _communicator.Start();
        }

        public void Send(string message)
        {
            SendInternal(message);
        }

        public void Send(RequestBase request)
        {
            var serialized = _serializer.Serialize(request);
            SendInternal(serialized);
        }

        private void SendInternal(string message)
        {
            try
            {
                _logger.LogDebug($"Sending client message: {message}");
                _communicator.Send(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while sending client message: '{message}'. Error: {ex.Message}");
                throw;
            }
        }

        private void HandleMessage(ResponseMessage responseMessage)
        {
            try
            {
                var message = ParseMessage(responseMessage);

                if (!message.StartsWith("{"))
                {
                    return;
                }

                var processed = TryHandleServerPingRequest(message)
                             || TryHandleUpdateMessages(message)
                             || TryHandlePullResponses(message)
                             || TryHandleSubscribeResponses(message)
                             || TryProcessErrorMessage(message);

                if (!processed)
                {
                    Streams.UnhandledMessageSubject.OnNext(message);
                    _logger.LogError($"Unhandled message received: {message}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while receiving authMessage");
            }
        }

        private bool TryHandleServerPingRequest(string message)
        {
            var pingRequest = _serializer.Deserialize<PingMessage>(message);
            if (pingRequest.Value > 0)
            {
                var clientResponse = new PongMessage(pingRequest.Value);
                var serialized = _serializer.Serialize(clientResponse);
                Send(serialized);
                return true;
            }

            //var serverRequest = _serializer.Deserialize<AuthMessage>(message);
            //if (string.Equals("ping", serverRequest.Action))
            //{
            //    var clientResponse = new AuthMessage("pong", serverRequest.Data);
            //    var serialized = _serializer.Serialize(clientResponse);
            //    Send(serialized);
            //    return true;
            //}

            return false;
        }

        private bool TryHandleUpdateMessages(string message)
        {
            if (MarketCandlestickUpdateMessage.TryParse(_serializer, message, out var marketCandlestick))
            {
                Streams.MarketCandlestickUpdateSubject.OnNext(marketCandlestick);
                return true;
            }

            if (MarketDepthUpdateMessage.TryParse(_serializer, message, out var marketDepth))
            {
                Streams.MarketDepthUpdateSubject.OnNext(marketDepth);
                return true;
            }

            return false;
        }

        private bool TryHandlePullResponses(string message)
        {
            if (MarketCandlestickPullResponse.TryParse(_serializer, message, out var marketCandlestick))
            {
                Streams.MarketCandlestickPullSubject.OnNext(marketCandlestick);
                return true;
            }

            if (MarketDepthPullResponse.TryParse(_serializer, message, out var marketDepth))
            {
                Streams.MarketDepthPullSubject.OnNext(marketDepth);
                return true;
            }

            return false;
        }

        private bool TryHandleSubscribeResponses(string message)
        {
            if (SubscribeResponse.TryParse(_serializer, message, out var subscribeResponse))
            {
                Streams.SubscribeResponseSubject.OnNext(subscribeResponse);
                return true;
            }

            if (UnsubscribeResponse.TryParse(_serializer, message, out var unsubscribeResponse))
            {
                Streams.UnsubscribeResponseSubject.OnNext(unsubscribeResponse);
                return true;
            }

            return false;
        }

        private bool TryProcessErrorMessage(string message)
        {
            if (ErrorMessage.TryParse(_serializer, message, out var errorMessage))
            {
                Streams.ErrorMessageSubject.OnNext(errorMessage);
                return true;
            }

            return false;
        }

        private static string ParseMessage(ResponseMessage message)
        {
            if (message.MessageType == WebSocketMessageType.Binary)
            {
                return Decompress(message.Binary);
            }

            return message.Text?.Trim() ?? string.Empty;
        }

        private static string Decompress(byte[] input)
        {
            using var inputStream = new MemoryStream(input);
            using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);

            using var outputStream = new MemoryStream();
            gZipStream.CopyTo(outputStream);

            var decompressed = Encoding.UTF8.GetString(outputStream.ToArray());
            return decompressed;
        }
    }
}