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
    internal class MarketClientExample
    {
        private readonly IHuobiMarketWebsocketClient _client;
        private readonly ILogger<MarketClientExample> _logger;

        private int _requestId;

        public MarketClientExample(IHuobiMarketWebsocketClient client, ILogger<MarketClientExample> symbol)
        {
            _client = client;
            _logger = symbol;
        }

        public async Task Execute(string symbol, int executionTimeMs = 10000)
        {
            SubscribeToStreams();

            await _client.Start();

            var tasks = new[]
            {
                CandlestickExample(symbol, executionTimeMs),
                DepthExample(symbol, executionTimeMs),
                MarketBestBidOfferExample(symbol, executionTimeMs),
                MarketByPriceExample(symbol, executionTimeMs),
                MarketTradeDetailExample(symbol, executionTimeMs),
                MarketDetailsExample(symbol, executionTimeMs)
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

            _client.Streams.CandlestickUpdateStream.Subscribe(HandleCandlestickUpdateMessage);
            _client.Streams.CandlestickPullStream.Subscribe(HandleCandlestickPullResponse);

            _client.Streams.DepthUpdateStream.Subscribe(HandleDepthUpdateMessage);
            _client.Streams.DepthPullStream.Subscribe(HandleDepthPullResponse);

            _client.Streams.MarketByPriceUpdateStream.Subscribe(HandleMarketByPriceUpdateMessage);
            _client.Streams.MarketByPriceRefreshUpdateStream.Subscribe(HandleMarketByPriceUpdateMessage);
            _client.Streams.MarketByPricePullStream.Subscribe(HandleMarketByPricePullResponse);

            _client.Streams.BestBidOfferUpdateStream.Subscribe(HandleBestBidOfferUpdateMessage);

            _client.Streams.TradeDetailUpdateStream.Subscribe(HandleTradeDetailUpdateMessage);
            _client.Streams.TradeDetailPullStream.Subscribe(HandleTradeDetailPullResponse);

            _client.Streams.MarketDetailsUpdateStream.Subscribe(HandleMarketDetailsUpdateMessage);
            _client.Streams.MarketDetailsPullStream.Subscribe(HandleMarketDetailsPullResponse);
        }

        private async Task CandlestickExample(string symbol, int executionTimeMs)
        {
            var marketCandlestickSubscribeRequest = new MarketCandlestickSubscribeRequest(
                symbol,
                MarketCandlestickPeriodType.OneMinute,
                GetNextId());
            _client.Send(marketCandlestickSubscribeRequest);

            await Task.Delay(executionTimeMs / 2);

            var now = new ZonedDateTime(Instant.FromDateTimeOffset(DateTimeOffset.UtcNow), DateTimeZone.Utc);
            var marketCandlestickPullRequest = new MarketCandlestickPullRequest(
                symbol,
                MarketCandlestickPeriodType.SixtyMinutes,
                GetNextId(),
                now.PlusHours(-5),
                now.PlusHours(-2));
            _client.Send(marketCandlestickPullRequest);

            await Task.Delay(executionTimeMs / 2);

            var candlestickUnsubscribeRequest = new MarketCandlestickUnsubscribeRequest(
                symbol,
                MarketCandlestickPeriodType.OneMinute,
                GetNextId());
            _client.Send(candlestickUnsubscribeRequest);
        }

        private async Task DepthExample(string symbol, int executionTimeMs)
        {
            var marketDepthSubscribeRequest =
                new MarketDepthSubscribeRequest(symbol, MarketDepthStepType.NoAggregation, GetNextId());
            _client.Send(marketDepthSubscribeRequest);

            await Task.Delay(executionTimeMs / 2);

            var marketDepthPullRequest = new MarketDepthPullRequest(
                symbol,
                MarketDepthStepType.NoAggregation,
                GetNextId());
            _client.Send(marketDepthPullRequest);

            await Task.Delay(executionTimeMs / 2);

            var depthUnsubscribeRequest =
                new MarketDepthUnsubscribeRequest(symbol, MarketDepthStepType.NoAggregation, GetNextId());
            _client.Send(depthUnsubscribeRequest);
        }

        private async Task MarketBestBidOfferExample(string symbol, int executionTimeMs)
        {
            var marketBestBidOfferSubscribeRequest = new MarketBestBidOfferSubscribeRequest(symbol, GetNextId());
            _client.Send(marketBestBidOfferSubscribeRequest);

            await Task.Delay(executionTimeMs);

            var marketBestBidOfferUnsubscribeRequest = new MarketBestBidOfferUnsubscribeRequest(symbol, GetNextId());
            _client.Send(marketBestBidOfferUnsubscribeRequest);
        }

        private async Task MarketByPriceExample(string symbol, int executionTimeMs)
        {
            var marketByPriceSubscribeRequest =
                new MarketByPriceSubscribeRequest(symbol, MarketByPriceLevelType.Five, GetNextId());
            _client.Send(marketByPriceSubscribeRequest);

            var marketByPriceRefreshSubscribeRequest = new MarketByPriceRefreshSubscribeRequest(
                symbol,
                MarketByPriceRefreshLevelType.Five,
                GetNextId());
            _client.Send(marketByPriceRefreshSubscribeRequest);

            await Task.Delay(executionTimeMs / 2);

            var marketByPricePullRequest = new MarketByPricePullRequest(
                symbol,
                MarketByPriceLevelType.Five,
                GetNextId());
            _client.Send(marketByPricePullRequest);

            await Task.Delay(executionTimeMs / 2);

            var marketByPriceUnsubscribeRequest =
                new MarketByPriceUnsubscribeRequest(symbol, MarketByPriceLevelType.Five, GetNextId());
            _client.Send(marketByPriceUnsubscribeRequest);

            var marketByPriceRefreshUnsubscribeRequest = new MarketByPriceRefreshUnsubscribeRequest(
                symbol,
                MarketByPriceRefreshLevelType.Five,
                GetNextId());
            _client.Send(marketByPriceRefreshUnsubscribeRequest);
        }

        private async Task MarketTradeDetailExample(string symbol, int executionTimeMs)
        {
            var marketTradeDetailSubscribeRequest = new MarketTradeDetailSubscribeRequest(symbol, GetNextId());
            _client.Send(marketTradeDetailSubscribeRequest);

            await Task.Delay(executionTimeMs / 2);

            var marketTradeDetailPullRequest = new MarketTradeDetailPullRequest(
                symbol,
                GetNextId());
            _client.Send(marketTradeDetailPullRequest);

            await Task.Delay(executionTimeMs / 2);

            var marketTradeDetailUnsubscribeRequest = new MarketTradeDetailUnsubscribeRequest(symbol, GetNextId());
            _client.Send(marketTradeDetailUnsubscribeRequest);
        }

        private async Task MarketDetailsExample(string symbol, int executionTimeMs)
        {
            var marketDetailsSubscribeRequest = new MarketDetailsSubscribeRequest(symbol, GetNextId());
            _client.Send(marketDetailsSubscribeRequest);

            await Task.Delay(executionTimeMs / 2);

            var marketDetailsPullRequest = new MarketDetailsPullRequest(
                symbol,
                GetNextId());
            _client.Send(marketDetailsPullRequest);

            await Task.Delay(executionTimeMs / 2);

            var marketDetailsUnsubscribeRequest = new MarketDetailsUnsubscribeRequest(symbol, GetNextId());
            _client.Send(marketDetailsUnsubscribeRequest);
        }

        private string GetNextId()
        {
            return $"id{_requestId++}";
        }

        private void HandleCandlestickUpdateMessage(MarketCandlestickUpdateMessage msg)
        {
            _logger.LogInformation(
                $"Market candlestick update {msg.Topic} | [amount={msg.Tick.Amount}] [open={msg.Tick.Open}] [close={msg.Tick.Close}] [low={msg.Tick.Low}] [high={msg.Tick.High}] [vol={msg.Tick.Vol}] [count={msg.Tick.Count}]");
        }

        private void HandleCandlestickPullResponse(MarketCandlestickPullResponse msg)
        {
            foreach (var item in msg.Data)
            {
                _logger.LogInformation(
                    $"Market candlestick pull {msg.Topic} | [amount={item.Amount}] [open={item.Open}] [close={item.Close}] [low={item.Low}] [high={item.High}] [vol={item.Vol}] [count={item.Count}]");
            }
        }

        private void HandleDepthUpdateMessage(MarketDepthUpdateMessage msg)
        {
            for (var i = 0; i < msg.Tick.Bids.Length && i < msg.Tick.Asks.Length; ++i)
            {
                var bid = msg.Tick.Bids[i];
                var ask = msg.Tick.Asks[i];

                _logger.LogInformation(
                    $"Market depth update {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}] [ask {i}: price={ask[0]} size={ask[1]}]");
            }
        }

        private void HandleDepthPullResponse(MarketDepthPullResponse msg)
        {
            for (var i = 0; i < msg.Data.Bids.Length && i < msg.Data.Asks.Length; ++i)
            {
                var bid = msg.Data.Bids[i];
                var ask = msg.Data.Asks[i];

                _logger.LogInformation(
                    $"Market depth update {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}] [ask {i}: price={ask[0]} size={ask[1]}]");
            }
        }

        private void HandleMarketByPriceUpdateMessage(UpdateMessage<MarketByPriceTick> msg)
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

        private void HandleMarketByPricePullResponse(MarketByPricePullResponse msg)
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

        private void HandleBestBidOfferUpdateMessage(MarketBestBidOfferUpdateMessage msg)
        {
            _logger.LogInformation(
                $"Market best bid/offer update {msg.Topic} | [symbol={msg.Tick.Symbol}] [quoteTime={msg.Tick.QuoteTime}] [bid={msg.Tick.Bid}] [bidSize={msg.Tick.BidSize}] [ask={msg.Tick.Ask}] [askSize={msg.Tick.AskSize}] [seqId={msg.Tick.SeqId}]");
        }

        private void HandleTradeDetailUpdateMessage(MarketTradeDetailUpdateMessage msg)
        {
            for (var i = 0; i < msg.Tick.Data.Length; ++i)
            {
                var item = msg.Tick.Data[i];

                _logger.LogInformation(
                    $"Market trade detail update {msg.Topic}, id={msg.Tick.Id}, ts={msg.Tick.Timestamp} | [item {i}: amount={item.Amount} ts={item.Timestamp} id={item.Id} tradeId={item.TradeId} price={item.Price} direction={item.Direction}]");
            }
        }

        private void HandleTradeDetailPullResponse(MarketTradeDetailPullResponse msg)
        {
            for (var i = 0; i < msg.Data.Length; ++i)
            {
                var item = msg.Data[i];

                _logger.LogInformation(
                    $"Market trade detail pull {msg.Topic} | [item {i}: amount={item.Amount} ts={item.Timestamp} id={item.Id} tradeId={item.TradeId} price={item.Price} direction={item.Direction}]");
            }
        }

        private void HandleMarketDetailsUpdateMessage(MarketDetailsUpdateMessage msg)
        {
            _logger.LogInformation(
                $"Market details update {msg.Topic} | [amount={msg.Tick.Amount}] [open={msg.Tick.Open}] [close={msg.Tick.Close}] [low={msg.Tick.Low}] [high={msg.Tick.High}] [vol={msg.Tick.Vol}] [count={msg.Tick.Count}]");
        }

        private void HandleMarketDetailsPullResponse(MarketDetailsPullResponse msg)
        {
            _logger.LogInformation(
                $"Market details pull {msg.Topic} | [amount={msg.Data.Amount}] [open={msg.Data.Open}] [close={msg.Data.Close}] [low={msg.Data.Low}] [high={msg.Data.High}] [vol={msg.Data.Vol}] [count={msg.Data.Count}]");
        }
    }
}