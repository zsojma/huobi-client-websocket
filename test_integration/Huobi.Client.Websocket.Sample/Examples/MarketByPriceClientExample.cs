using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Sample.Examples
{
    internal class MarketByPriceClientExample : IExample
    {
        private readonly IHuobiMarketByPriceWebsocketClient _client;
        private readonly ILogger<MarketByPriceClientExample> _logger;

        private int _requestId;

        public MarketByPriceClientExample(IHuobiMarketByPriceWebsocketClient client, ILogger<MarketByPriceClientExample> symbol)
        {
            _client = client;
            _logger = symbol;
        }

        public async Task Start(string symbol)
        {
            SubscribeToStreams();

            await _client.Start();
            await StartMarketByPriceExample(symbol);
        }

        public Task Stop(string symbol)
        {
            return StopMarketByPriceExample(symbol);
        }

        private void SubscribeToStreams()
        {
            _client.Streams.UnhandledMessageStream.Subscribe(x => _logger.LogError($"Unhandled message: {x}"));
            _client.Streams.ErrorMessageStream.Subscribe(
                x => _logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x.Message}"));
            _client.Streams.SubscribeResponseStream.Subscribe(x => _logger.LogInformation($"Subscribed to topic: {x.Topic}"));
            _client.Streams.UnsubscribeResponseStream.Subscribe(
                x => _logger.LogInformation($"Unsubscribed from topic: {x.Topic}"));

            _client.Streams.MarketByPriceUpdateStream.Subscribe(Handle);
            _client.Streams.MarketByPricePullStream.Subscribe(Handle);
        }

        private async Task StartMarketByPriceExample(string symbol)
        {
            var subscribeRequest =
                new MarketByPriceSubscribeRequest(GetNextId(), symbol, MarketByPriceLevelType.Five);
            _client.Send(subscribeRequest);

            await Task.Delay(1000);

            var pullRequest = new MarketByPricePullRequest(
                GetNextId(),
                symbol,
                MarketByPriceLevelType.Five);
            _client.Send(pullRequest);
        }

        private Task StopMarketByPriceExample(string symbol)
        {
            var unsubscribeRequest =
                new MarketByPriceUnsubscribeRequest(GetNextId(), symbol, MarketByPriceLevelType.Five);
            _client.Send(unsubscribeRequest);

            return Task.CompletedTask;
        }

        private string GetNextId()
        {
            return $"id{_requestId++}";
        }

        private void Handle(MarketByPriceUpdateMessage msg)
        {
            if (msg.Tick.Bids != null)
            {
                for (var i = 0; i < msg.Tick.Bids.Length; ++i)
                {
                    var bid = msg.Tick.Bids[i];

                    _logger.LogInformation($"Market by price update {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}]");
                }
            }

            if (msg.Tick.Asks != null)
            {
                for (var i = 0; i < msg.Tick.Asks.Length; ++i)
                {
                    var bid = msg.Tick.Asks[i];

                    _logger.LogInformation($"Market by price update {msg.Topic} | [ask {i}: price={bid[0]} size={bid[1]}]");
                }
            }
        }

        private void Handle(MarketByPricePullResponse msg)
        {
            for (var i = 0; i < msg.Data.Bids.Length; ++i)
            {
                var bid = msg.Data.Bids[i];

                _logger.LogInformation($"Market by price pull {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}]");
            }

            for (var i = 0; i < msg.Data.Asks.Length; ++i)
            {
                var bid = msg.Data.Asks[i];

                _logger.LogInformation($"Market by price pull {msg.Topic} | [ask {i}: price={bid[0]} size={bid[1]}]");
            }
        }
    }
}