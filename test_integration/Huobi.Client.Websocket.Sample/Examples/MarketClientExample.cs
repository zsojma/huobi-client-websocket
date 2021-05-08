using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.MarketDepth;
using Huobi.Client.Websocket.Messages.MarketData.MarketDetails;
using Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Microsoft.Extensions.Logging;

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
                StartMarketByPriceExample(symbol),
                StartMarketBestBidOfferExample(symbol),
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
                StopMarketByPriceExample(symbol),
                StopMarketBestBidOfferExample(symbol),
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
            
            _client.Streams.MarketByPriceRefreshUpdateStream.Subscribe(Handle);

            _client.Streams.BestBidOfferUpdateStream.Subscribe(Handle);

            _client.Streams.TradeDetailUpdateStream.Subscribe(Handle);
            _client.Streams.TradeDetailPullStream.Subscribe(Handle);

            _client.Streams.MarketDetailsUpdateStream.Subscribe(Handle);
            _client.Streams.MarketDetailsPullStream.Subscribe(Handle);
        }

        private async Task StartCandlestickExample(string symbol)
        {
            var subscribeRequest = new MarketCandlestickSubscribeRequest(
                GetNextId(),
                symbol,
                MarketCandlestickPeriodType.OneMinute);
            _client.Send(subscribeRequest);

            await Task.Delay(1000);

            var now = DateTimeOffset.UtcNow;
            var pullRequest = new MarketCandlestickPullRequest(
                GetNextId(),
                symbol,
                MarketCandlestickPeriodType.SixtyMinutes,
                now.AddHours(-5),
                now.AddHours(-2));
            _client.Send(pullRequest);
        }

        private Task StopCandlestickExample(string symbol)
        {
            var unsubscribeRequest = new MarketCandlestickUnsubscribeRequest(
                GetNextId(),
                symbol,
                MarketCandlestickPeriodType.OneMinute);
            _client.Send(unsubscribeRequest);

            return Task.CompletedTask;
        }

        private async Task StartDepthExample(string symbol)
        {
            var subscribeRequest =
                new MarketDepthSubscribeRequest(GetNextId(), symbol, 0);
            _client.Send(subscribeRequest);

            await Task.Delay(1000);

            var pullRequest = new MarketDepthPullRequest(
                GetNextId(),
                symbol,
                1);
            _client.Send(pullRequest);
        }

        private Task StopDepthExample(string symbol)
        {
            var unsubscribeRequest =
                new MarketDepthUnsubscribeRequest(GetNextId(), symbol, 0);
            _client.Send(unsubscribeRequest);

            return Task.CompletedTask;
        }

        private Task StartMarketByPriceExample(string symbol)
        {
            var marketByPriceRefreshSubscribeRequest = new MarketByPriceRefreshSubscribeRequest(
                GetNextId(),
                symbol,
                5);
            _client.Send(marketByPriceRefreshSubscribeRequest);

            return Task.CompletedTask;
        }

        private Task StopMarketByPriceExample(string symbol)
        {
            var marketByPriceRefreshUnsubscribeRequest = new MarketByPriceRefreshUnsubscribeRequest(
                GetNextId(),
                symbol,
                5);
            _client.Send(marketByPriceRefreshUnsubscribeRequest);

            return Task.CompletedTask;
        }

        private Task StartMarketBestBidOfferExample(string symbol)
        {
            var subscribeRequest = new MarketBestBidOfferSubscribeRequest(GetNextId(), symbol);
            _client.Send(subscribeRequest);

            return Task.CompletedTask;
        }

        private Task StopMarketBestBidOfferExample(string symbol)
        {
            var unsubscribeRequest = new MarketBestBidOfferUnsubscribeRequest(GetNextId(), symbol);
            _client.Send(unsubscribeRequest);

            return Task.CompletedTask;
        }

        private async Task StartMarketTradeDetailExample(string symbol)
        {
            var subscribeRequest = new MarketTradeDetailSubscribeRequest(GetNextId(), symbol);
            _client.Send(subscribeRequest);

            await Task.Delay(1000);

            var pullRequest = new MarketTradeDetailPullRequest(
                GetNextId(),
                symbol);
            _client.Send(pullRequest);
        }

        private Task StopMarketTradeDetailExample(string symbol)
        {
            var unsubscribeRequest = new MarketTradeDetailUnsubscribeRequest(GetNextId(), symbol);
            _client.Send(unsubscribeRequest);

            return Task.CompletedTask;
        }

        private async Task StartMarketDetailsExample(string symbol)
        {
            var subscribeRequest = new MarketDetailsSubscribeRequest(GetNextId(), symbol);
            _client.Send(subscribeRequest);

            await Task.Delay(1000);

            var pullRequest = new MarketDetailsPullRequest(
                GetNextId(),
                symbol);
            _client.Send(pullRequest);
        }

        private Task StopMarketDetailsExample(string symbol)
        {
            var unsubscribeRequest = new MarketDetailsUnsubscribeRequest(GetNextId(), symbol);
            _client.Send(unsubscribeRequest);

            return Task.CompletedTask;
        }

        private string GetNextId()
        {
            return $"id{_requestId++}";
        }

        private void Handle(MarketCandlestickUpdateMessage msg)
        {
            if (msg.Tick is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Market candlestick update {msg.Topic} | [amount={msg.Tick.Amount}] [open={msg.Tick.Open}] [close={msg.Tick.Close}] [low={msg.Tick.Low}] [high={msg.Tick.High}] [vol={msg.Tick.Vol}] [count={msg.Tick.Count}]");
        }

        private void Handle(MarketCandlestickPullResponse msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            foreach (var item in msg.Data)
            {
                _logger.LogInformation(
                    $"Market candlestick pull {msg.Topic} | [amount={item.Amount}] [open={item.Open}] [close={item.Close}] [low={item.Low}] [high={item.High}] [vol={item.Vol}] [count={item.Count}]");
            }
        }

        private void Handle(MarketDepthUpdateMessage msg)
        {
            var len = Math.Min(Math.Min(10, msg.Tick?.Bids.Length ?? 0), msg.Tick?.Asks.Length ?? 0);

            for (var i = 0; i < len; ++i)
            {
                var bid = msg.Tick!.Bids[i];
                var ask = msg.Tick!.Asks[i];

                _logger.LogInformation(
                    $"Market depth update {msg.Topic} | [bid {i}: price={bid.Price} size={bid.Size}] [ask {i}: price={ask.Price} size={ask.Size}]");
            }
        }

        private void Handle(MarketDepthPullResponse msg)
        {
            var len = Math.Min(Math.Min(10, msg.Data?.Bids.Length ?? 0), msg.Data?.Asks.Length ?? 0);

            for (var i = 0; i < len; ++i)
            {
                var bid = msg.Data!.Bids[i];
                var ask = msg.Data!.Asks[i];

                _logger.LogInformation(
                    $"Market depth update {msg.Topic} | [bid {i}: price={bid.Price} size={bid.Size}] [ask {i}: price={ask.Price} size={ask.Size}]");
            }
        }

        private void Handle(MarketByPriceRefreshUpdateMessage msg)
        {
            if (msg.Tick?.Bids != null)
            {
                for (var i = 0; i < msg.Tick.Bids.Length; ++i)
                {
                    var bid = msg.Tick.Bids[i];

                    _logger.LogInformation($"Market by price update {msg.Topic} | [bid {i}: price={bid.Price} size={bid.Size}]");
                }
            }

            if (msg.Tick?.Asks != null)
            {
                for (var i = 0; i < msg.Tick.Asks.Length; ++i)
                {
                    var bid = msg.Tick.Asks[i];

                    _logger.LogInformation($"Market by price update {msg.Topic} | [ask {i}: price={bid.Price} size={bid.Size}]");
                }
            }
        }

        private void Handle(MarketBestBidOfferUpdateMessage msg)
        {
            if (msg.Tick is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Market best bid/offer update {msg.Topic} | [symbol={msg.Tick.Symbol}] [quoteTime={msg.Tick.QuoteTime}] [bid={msg.Tick.Bid}] [bidSize={msg.Tick.BidSize}] [ask={msg.Tick.Ask}] [askSize={msg.Tick.AskSize}] [seqId={msg.Tick.SeqId}]");
        }

        private void Handle(MarketTradeDetailUpdateMessage msg)
        {
            for (var i = 0; i < msg.Tick?.Data.Length; ++i)
            {
                var item = msg.Tick.Data[i];

                _logger.LogInformation(
                    $"Market trade detail update {msg.Topic}, id={msg.Tick.Id}, ts={msg.Tick.Timestamp} | [item {i}: amount={item.Amount} ts={item.Timestamp} id={item.Id} tradeId={item.TradeId} price={item.Price} direction={item.Direction}]");
            }
        }

        private void Handle(MarketTradeDetailPullResponse msg)
        {
            for (var i = 0; i < msg.Data?.Length; ++i)
            {
                var item = msg.Data[i];

                _logger.LogInformation(
                    $"Market trade detail pull {msg.Topic} | [item {i}: amount={item.Amount} ts={item.Timestamp} id={item.Id} tradeId={item.TradeId} price={item.Price} direction={item.Direction}]");
            }
        }

        private void Handle(MarketDetailsUpdateMessage msg)
        {
            if (msg.Tick is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Market details update {msg.Topic} | [amount={msg.Tick.Amount}] [open={msg.Tick.Open}] [close={msg.Tick.Close}] [low={msg.Tick.Low}] [high={msg.Tick.High}] [vol={msg.Tick.Vol}] [count={msg.Tick.Count}]");
        }

        private void Handle(MarketDetailsPullResponse msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Market details pull {msg.Topic} | [amount={msg.Data.Amount}] [open={msg.Data.Open}] [close={msg.Data.Close}] [low={msg.Data.Low}] [high={msg.Data.High}] [vol={msg.Data.Vol}] [count={msg.Data.Count}]");
        }
    }
}