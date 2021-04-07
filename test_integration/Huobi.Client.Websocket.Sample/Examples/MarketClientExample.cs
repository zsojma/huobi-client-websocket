using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.MarketDepth;
using Huobi.Client.Websocket.Messages.MarketData.MarketDetails;
using Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace Huobi.Client.Websocket.Sample.Examples
{
    internal class MarketClientExample : IExample
    {
        private readonly IHuobiMarketWebsocketClient _client;
        private readonly ILogger<MarketClientExample> _logger;

        private int _requestId;

        public MarketClientExample(IHuobiMarketWebsocketClient client, ILogger<MarketClientExample> symbol)
        {
            _client = client;
            _logger = symbol;
        }

        public async Task Start(string symbol)
        {
            SubscribeToStreams();

            await _client.Start();

            var tasks = new[]
            {
                StartCandlestickExample(symbol),
                StartDepthExample(symbol),
                StartMarketBestBidOfferExample(symbol),
                StartMarketByPriceExample(symbol),
                StartMarketTradeDetailExample(symbol),
                StartMarketDetailsExample(symbol)
            };

            await Task.WhenAll(tasks);
        }

        public async Task Stop(string symbol)
        {
            var tasks = new[]
            {
                StopCandlestickExample(symbol),
                StopDepthExample(symbol),
                StopMarketBestBidOfferExample(symbol),
                StopMarketByPriceExample(symbol),
                StopMarketTradeDetailExample(symbol),
                StopMarketDetailsExample(symbol)
            };

            await Task.WhenAll(tasks);
        }

        private void SubscribeToStreams()
        {
            _client.Streams.UnhandledMessageStream.Subscribe(x => _logger.LogError($"Unhandled message: {x}"));
            _client.Streams.ErrorMessageStream.Subscribe(
                x => _logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x.Message}"));
            _client.Streams.SubscribeResponseStream.Subscribe(x => _logger.LogInformation($"Subscribed to topic: {x.Topic}"));
            _client.Streams.UnsubscribeResponseStream.Subscribe(
                x => _logger.LogInformation($"Unsubscribed from topic: {x.Topic}"));

            _client.Streams.CandlestickUpdateStream.Subscribe(Handle);
            _client.Streams.CandlestickPullStream.Subscribe(Handle);

            _client.Streams.DepthUpdateStream.Subscribe(Handle);
            _client.Streams.DepthPullStream.Subscribe(Handle);

            _client.Streams.MarketByPriceUpdateStream.Subscribe(Handle);
            _client.Streams.MarketByPriceRefreshUpdateStream.Subscribe(Handle);
            _client.Streams.MarketByPricePullStream.Subscribe(Handle);

            _client.Streams.BestBidOfferUpdateStream.Subscribe(Handle);

            _client.Streams.TradeDetailUpdateStream.Subscribe(Handle);
            _client.Streams.TradeDetailPullStream.Subscribe(Handle);

            _client.Streams.MarketDetailsUpdateStream.Subscribe(Handle);
            _client.Streams.MarketDetailsPullStream.Subscribe(Handle);
        }

        private async Task StartCandlestickExample(string symbol)
        {
            var marketCandlestickSubscribeRequest = new MarketCandlestickSubscribeRequest(
                symbol,
                MarketCandlestickPeriodType.OneMinute,
                GetNextId());
            _client.Send(marketCandlestickSubscribeRequest);

            await Task.Delay(1000);

            var now = new ZonedDateTime(Instant.FromDateTimeOffset(DateTimeOffset.UtcNow), DateTimeZone.Utc);
            var marketCandlestickPullRequest = new MarketCandlestickPullRequest(
                symbol,
                MarketCandlestickPeriodType.SixtyMinutes,
                GetNextId(),
                now.PlusHours(-5),
                now.PlusHours(-2));
            _client.Send(marketCandlestickPullRequest);
        }

        private Task StopCandlestickExample(string symbol)
        {
            var candlestickUnsubscribeRequest = new MarketCandlestickUnsubscribeRequest(
                symbol,
                MarketCandlestickPeriodType.OneMinute,
                GetNextId());
            _client.Send(candlestickUnsubscribeRequest);

            return Task.CompletedTask;
        }

        private async Task StartDepthExample(string symbol)
        {
            var marketDepthSubscribeRequest =
                new MarketDepthSubscribeRequest(symbol, MarketDepthStepType.NoAggregation, GetNextId());
            _client.Send(marketDepthSubscribeRequest);

            await Task.Delay(1000);

            var marketDepthPullRequest = new MarketDepthPullRequest(
                symbol,
                MarketDepthStepType.NoAggregation,
                GetNextId());
            _client.Send(marketDepthPullRequest);
        }

        private Task StopDepthExample(string symbol)
        {
            var depthUnsubscribeRequest =
                new MarketDepthUnsubscribeRequest(symbol, MarketDepthStepType.NoAggregation, GetNextId());
            _client.Send(depthUnsubscribeRequest);

            return Task.CompletedTask;
        }

        private Task StartMarketBestBidOfferExample(string symbol)
        {
            var marketBestBidOfferSubscribeRequest = new MarketBestBidOfferSubscribeRequest(symbol, GetNextId());
            _client.Send(marketBestBidOfferSubscribeRequest);

            return Task.CompletedTask;
        }

        private Task StopMarketBestBidOfferExample(string symbol)
        {
            var marketBestBidOfferUnsubscribeRequest = new MarketBestBidOfferUnsubscribeRequest(symbol, GetNextId());
            _client.Send(marketBestBidOfferUnsubscribeRequest);

            return Task.CompletedTask;
        }

        private async Task StartMarketByPriceExample(string symbol)
        {
            var marketByPriceSubscribeRequest =
                new MarketByPriceSubscribeRequest(symbol, MarketByPriceLevelType.Five, GetNextId());
            _client.Send(marketByPriceSubscribeRequest);

            var marketByPriceRefreshSubscribeRequest = new MarketByPriceRefreshSubscribeRequest(
                symbol,
                MarketByPriceRefreshLevelType.Five,
                GetNextId());
            _client.Send(marketByPriceRefreshSubscribeRequest);

            await Task.Delay(1000);

            var marketByPricePullRequest = new MarketByPricePullRequest(
                symbol,
                MarketByPriceLevelType.Five,
                GetNextId());
            _client.Send(marketByPricePullRequest);
        }

        private Task StopMarketByPriceExample(string symbol)
        {
            var marketByPriceUnsubscribeRequest =
                new MarketByPriceUnsubscribeRequest(symbol, MarketByPriceLevelType.Five, GetNextId());
            _client.Send(marketByPriceUnsubscribeRequest);

            var marketByPriceRefreshUnsubscribeRequest = new MarketByPriceRefreshUnsubscribeRequest(
                symbol,
                MarketByPriceRefreshLevelType.Five,
                GetNextId());
            _client.Send(marketByPriceRefreshUnsubscribeRequest);

            return Task.CompletedTask;
        }

        private async Task StartMarketTradeDetailExample(string symbol)
        {
            var marketTradeDetailSubscribeRequest = new MarketTradeDetailSubscribeRequest(symbol, GetNextId());
            _client.Send(marketTradeDetailSubscribeRequest);

            await Task.Delay(1000);

            var marketTradeDetailPullRequest = new MarketTradeDetailPullRequest(
                symbol,
                GetNextId());
            _client.Send(marketTradeDetailPullRequest);
        }

        private Task StopMarketTradeDetailExample(string symbol)
        {
            var marketTradeDetailUnsubscribeRequest = new MarketTradeDetailUnsubscribeRequest(symbol, GetNextId());
            _client.Send(marketTradeDetailUnsubscribeRequest);

            return Task.CompletedTask;
        }

        private async Task StartMarketDetailsExample(string symbol)
        {
            var marketDetailsSubscribeRequest = new MarketDetailsSubscribeRequest(symbol, GetNextId());
            _client.Send(marketDetailsSubscribeRequest);

            await Task.Delay(1000);

            var marketDetailsPullRequest = new MarketDetailsPullRequest(
                symbol,
                GetNextId());
            _client.Send(marketDetailsPullRequest);
        }

        private Task StopMarketDetailsExample(string symbol)
        {
            var marketDetailsUnsubscribeRequest = new MarketDetailsUnsubscribeRequest(symbol, GetNextId());
            _client.Send(marketDetailsUnsubscribeRequest);

            return Task.CompletedTask;
        }

        private string GetNextId()
        {
            return $"id{_requestId++}";
        }

        private void Handle(MarketCandlestickUpdateMessage msg)
        {
            _logger.LogInformation(
                $"Market candlestick update {msg.Topic} | [amount={msg.Tick.Amount}] [open={msg.Tick.Open}] [close={msg.Tick.Close}] [low={msg.Tick.Low}] [high={msg.Tick.High}] [vol={msg.Tick.Vol}] [count={msg.Tick.Count}]");
        }

        private void Handle(MarketCandlestickPullResponse msg)
        {
            foreach (var item in msg.Data)
            {
                _logger.LogInformation(
                    $"Market candlestick pull {msg.Topic} | [amount={item.Amount}] [open={item.Open}] [close={item.Close}] [low={item.Low}] [high={item.High}] [vol={item.Vol}] [count={item.Count}]");
            }
        }

        private void Handle(MarketDepthUpdateMessage msg)
        {
            for (var i = 0; i < msg.Tick.Bids.Length && i < msg.Tick.Asks.Length; ++i)
            {
                var bid = msg.Tick.Bids[i];
                var ask = msg.Tick.Asks[i];

                _logger.LogInformation(
                    $"Market depth update {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}] [ask {i}: price={ask[0]} size={ask[1]}]");
            }
        }

        private void Handle(MarketDepthPullResponse msg)
        {
            for (var i = 0; i < msg.Data.Bids.Length && i < msg.Data.Asks.Length; ++i)
            {
                var bid = msg.Data.Bids[i];
                var ask = msg.Data.Asks[i];

                _logger.LogInformation(
                    $"Market depth update {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}] [ask {i}: price={ask[0]} size={ask[1]}]");
            }
        }

        private void Handle(UpdateMessage<MarketByPriceTick> msg)
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

        private void Handle(MarketBestBidOfferUpdateMessage msg)
        {
            _logger.LogInformation(
                $"Market best bid/offer update {msg.Topic} | [symbol={msg.Tick.Symbol}] [quoteTime={msg.Tick.QuoteTime}] [bid={msg.Tick.Bid}] [bidSize={msg.Tick.BidSize}] [ask={msg.Tick.Ask}] [askSize={msg.Tick.AskSize}] [seqId={msg.Tick.SeqId}]");
        }

        private void Handle(MarketTradeDetailUpdateMessage msg)
        {
            for (var i = 0; i < msg.Tick.Data.Length; ++i)
            {
                var item = msg.Tick.Data[i];

                _logger.LogInformation(
                    $"Market trade detail update {msg.Topic}, id={msg.Tick.Id}, ts={msg.Tick.Timestamp} | [item {i}: amount={item.Amount} ts={item.Timestamp} id={item.Id} tradeId={item.TradeId} price={item.Price} direction={item.Direction}]");
            }
        }

        private void Handle(MarketTradeDetailPullResponse msg)
        {
            for (var i = 0; i < msg.Data.Length; ++i)
            {
                var item = msg.Data[i];

                _logger.LogInformation(
                    $"Market trade detail pull {msg.Topic} | [item {i}: amount={item.Amount} ts={item.Timestamp} id={item.Id} tradeId={item.TradeId} price={item.Price} direction={item.Direction}]");
            }
        }

        private void Handle(MarketDetailsUpdateMessage msg)
        {
            _logger.LogInformation(
                $"Market details update {msg.Topic} | [amount={msg.Tick.Amount}] [open={msg.Tick.Open}] [close={msg.Tick.Close}] [low={msg.Tick.Low}] [high={msg.Tick.High}] [vol={msg.Tick.Vol}] [count={msg.Tick.Count}]");
        }

        private void Handle(MarketDetailsPullResponse msg)
        {
            _logger.LogInformation(
                $"Market details pull {msg.Topic} | [amount={msg.Data.Amount}] [open={msg.Data.Open}] [close={msg.Data.Close}] [low={msg.Data.Low}] [high={msg.Data.High}] [vol={msg.Data.Vol}] [count={msg.Data.Count}]");
        }
    }
}