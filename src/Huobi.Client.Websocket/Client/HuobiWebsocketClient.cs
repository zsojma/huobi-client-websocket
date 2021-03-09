using System;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages;
using Huobi.Client.Websocket.Messages.Subscription;
using Huobi.Client.Websocket.Messages.Subscription.Ticks;
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

        public HuobiClientStreams Streams { get; } = new HuobiClientStreams();

        public void Dispose()
        {
            _communicator.Dispose();
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

                if (message.StartsWith("{") && TryHandleObjectMessage(message))
                {
                    return;
                }

                if (TryHandleRawMessage(message))
                {
                    return;
                }

                if (!string.IsNullOrWhiteSpace(message))
                {
                    Streams.UnhandledMessageSubject.OnNext(message);
                }

                _logger.LogWarning($"Unhandled response: '{message}'");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while receiving authMessage");
            }
        }

        private bool TryHandleObjectMessage(string input)
        {
            if (TryHandleServerPingRequest(input))
            {
                return true;
            }

            if (SubscribeResponse.TryParse(_serializer, input, out var response))
            {
                Streams.SubscribeResponseSubject.OnNext(response);
                return true;
            }

            if (MarketCandlestickTick.TryParse(_serializer, input, out var marketCandlestick))
            {
                Streams.MarketCandlestickSubject.OnNext(marketCandlestick);
                return true;
            }

            if (MarketDepthTick.TryParse(_serializer, input, out var marketDepth))
            {
                Streams.MarketDepthSubject.OnNext(marketDepth);
                return true;
            }

            return false;
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

            var serverRequest = _serializer.Deserialize<AuthMessage>(message);
            if (string.Equals("ping", serverRequest.Action))
            {
                var clientResponse = new AuthMessage("pong", serverRequest.Data);
                var serialized = _serializer.Serialize(clientResponse);
                Send(serialized);
                return true;
            }

            return false;
        }

        private bool TryHandleRawMessage(string message)
        {
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