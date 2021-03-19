using System;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages;
using Huobi.Client.Websocket.Messages.Pulling.MarketByPrice;
using Huobi.Client.Websocket.Messages.Pulling.MarketCandlestick;
using Huobi.Client.Websocket.Messages.Pulling.MarketDepth;
using Huobi.Client.Websocket.Messages.Pulling.MarketDetails;
using Huobi.Client.Websocket.Messages.Pulling.MarketTradeDetail;
using Huobi.Client.Websocket.Messages.Subscription;
using Huobi.Client.Websocket.Messages.Subscription.MarketBestBidOffer;
using Huobi.Client.Websocket.Messages.Subscription.MarketByPrice;
using Huobi.Client.Websocket.Messages.Subscription.MarketCandlestick;
using Huobi.Client.Websocket.Messages.Subscription.MarketDepth;
using Huobi.Client.Websocket.Messages.Subscription.MarketDetails;
using Huobi.Client.Websocket.Messages.Subscription.MarketTradeDetail;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging;
using Websocket.Client;

namespace Huobi.Client.Websocket.Client
{
    internal class HuobiWebsocketClient : IHuobiWebsocketClient
    {
        private readonly IHuobiSerializer _serializer;
        private readonly ILogger<HuobiWebsocketClient> _logger;

        private readonly IDisposable _messageReceivedSubscription;

        public HuobiWebsocketClient(
            IHuobiWebsocketCommunicator communicator,
            IHuobiSerializer serializer,
            ILogger<HuobiWebsocketClient> logger)
        {
            Communicator = communicator;
            _serializer = serializer;
            _logger = logger;

            _messageReceivedSubscription = Communicator.MessageReceived.Subscribe(HandleMessage);
        }

        public IHuobiWebsocketCommunicator Communicator { get; }

        public HuobiClientStreams Streams { get; } = new();

        public void Dispose()
        {
            _messageReceivedSubscription.Dispose();
        }

        public void Send(RequestBase request)
        {
            var serialized = _serializer.Serialize(request);
            SendInternal(serialized);
        }

        public void Send(string message)
        {
            SendInternal(message);
        }

        private void SendInternal(string message)
        {
            try
            {
                _logger.LogDebug($"Sending client message: {message}");
                Communicator.Send(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while sending client message: '{message}'. Error: {ex.Message}");
                throw;
            }
        }

        private void HandleMessage(ResponseMessage responseMessage)
        {
            var message = ParseMessage(responseMessage);

            try
            {
                var processed = false;
                if (message.StartsWith("{"))
                {
                    processed = TryHandleServerPingRequest(message)
                             || TryHandlePullResponses(message)
                             || TryHandleUpdateMessages(message)
                             || TryHandleSubscribeResponses(message)
                             || TryProcessErrorMessage(message);
                }

                if (!processed)
                {
                    Streams.UnhandledMessageSubject.OnNext(message);
                    _logger.LogError($"Unhandled message received: {message}");
                }
            }
            catch (Exception e)
            {
                Streams.UnhandledMessageSubject.OnNext(message);
                _logger.LogError(e, "Exception while processing of response message");
            }
        }

        private bool TryHandleServerPingRequest(string message)
        {
            if (_serializer.TryDeserializeIfContains<PingMessage>(message, "ping", out var pingRequest))
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

            if (MarketByPricePullResponse.TryParse(_serializer, message, out var marketByPrice))
            {
                Streams.MarketByPricePullSubject.OnNext(marketByPrice);
                return true;
            }

            if (MarketTradeDetailPullResponse.TryParse(_serializer, message, out var marketTradeDetail))
            {
                Streams.MarketTradeDetailPullSubject.OnNext(marketTradeDetail);
                return true;
            }

            if (MarketDetailsPullResponse.TryParse(_serializer, message, out var marketDetails))
            {
                Streams.MarketDetailsPullSubject.OnNext(marketDetails);
                return true;
            }

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

            if (MarketByPriceUpdateMessage.TryParse(_serializer, message, out var marketByPrice))
            {
                Streams.MarketByPriceUpdateSubject.OnNext(marketByPrice);
                return true;
            }

            if (MarketByPriceRefreshUpdateMessage.TryParse(_serializer, message, out var marketByPriceRefresh))
            {
                Streams.MarketByPriceRefreshUpdateSubject.OnNext(marketByPriceRefresh);
                return true;
            }

            if (MarketBestBidOfferUpdateMessage.TryParse(_serializer, message, out var marketBestBidOffer))
            {
                Streams.MarketBestBidOfferUpdateSubject.OnNext(marketBestBidOffer);
                return true;
            }

            if (MarketTradeDetailUpdateMessage.TryParse(_serializer, message, out var marketTradeDetail))
            {
                Streams.MarketTradeDetailUpdateSubject.OnNext(marketTradeDetail);
                return true;
            }

            if (MarketDetailsUpdateMessage.TryParse(_serializer, message, out var marketDetails))
            {
                Streams.MarketDetailsUpdateSubject.OnNext(marketDetails);
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

        private string ParseMessage(ResponseMessage message)
        {
            try
            {
                if (message.MessageType == WebSocketMessageType.Binary)
                {
                    return Decompress(message.Binary);
                }

                return message.Text?.Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while parsing response message. Error: " + ex.Message);
                return string.Empty;
            }
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