using System;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Messages.Pulling.Candlestick;
using Huobi.Client.Websocket.Messages.Pulling.Depth;
using Huobi.Client.Websocket.Messages.Subscription.Candlestick;
using Huobi.Client.Websocket.Messages.Subscription.Depth;
using Huobi.Client.Websocket.Messages.Values;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace Huobi.Client.Websocket.Sample
{
    public class Program
    {
        private const string ACCESS_KEY = "1abca636-5540151d-bewr5drtmh-18574";
        private const string SECRET_KEY = "e91b0e46-9cec52db-3bc147c3-c4276";
        
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);

        public static async Task Main()
        {
            // setup DI
            var serviceCollection = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .AddHuobiWebsocketServices();

            serviceCollection
                .AddOptions<HuobiWebsocketClientConfig>()
                .Configure(
                    x =>
                    {
                        x.Url = HuobiConstants.ApiWebsocketUrl;
                        x.Name = "Huobi-1";
                        x.ReconnectTimeoutMinutes = 10;
                    });

            var serviceProvider = serviceCollection
                .BuildServiceProvider();

            // configure console logging
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("Starting application");

            using var client = serviceProvider.GetRequiredService<IHuobiWebsocketClient>();
            
            client.Streams.UnhandledMessageStream.Subscribe(x => { logger.LogError($"Unhandled message: {x}"); });
            client.Streams.ErrorMessageStream.Subscribe(x => { logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x.Message}"); });
            
            client.Streams.SubscribeResponseStream.Subscribe(x => { logger.LogInformation($"Subscribed to topic: {x.Topic}"); });
            client.Streams.UnsubscribeResponseStream.Subscribe(x => { logger.LogInformation($"Unsubscribed from topic: {x.Topic}"); });
            
            client.Streams.MarketCandlestickUpdateStream.Subscribe(
                msg =>
                {
                    logger.LogInformation($"Candle update {msg.Topic} | [amount={msg.Tick.Amount}] [open={msg.Tick.Open}] [close={msg.Tick.Close}] [low={msg.Tick.Low}] [high={msg.Tick.High}] [vol={msg.Tick.Vol}] [count={msg.Tick.Count}]");
                });
            
            client.Streams.MarketCandlestickPullStream.Subscribe(
                msg =>
                {
                    for (var i = 0; i < msg.Data.Length; ++i)
                    {
                        var item = msg.Data[i];
                        logger.LogInformation(
                            $"Candle pull {msg.Topic} | [amount={item.Amount}] [open={item.Open}] [close={item.Close}] [low={item.Low}] [high={item.High}] [vol={item.Vol}] [count={item.Count}]");
                    }
                });
            
            client.Streams.MarketDepthUpdateStream.Subscribe(
                msg =>
                {
                    for (var i = 0; i < msg.Tick.Bids.Length; ++i)
                    {
                        var bid = msg.Tick.Bids[i];
                        var ask = msg.Tick.Asks[i];

                        logger.LogInformation($"Depth update {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}] [ask {i}: price={ask[0]} size={ask[1]}]");
                    }
                });

            client.Streams.MarketDepthPullStream.Subscribe(
                msg =>
                {
                    for (var i = 0; i < msg.Data.Bids.Length; ++i)
                    {
                        var bid = msg.Data.Bids[i];
                        var ask = msg.Data.Asks[i];

                        logger.LogInformation($"Depth update {msg.Topic} | [bid {i}: price={bid[0]} size={bid[1]}] [ask {i}: price={ask[0]} size={ask[1]}]");
                    }
                });

            var marketCandlestickSubscribeRequest = new MarketCandlestickSubscribeRequest("btcusdt", MarketCandlestickPeriodType.OneMinute, "id1");
            client.Send(marketCandlestickSubscribeRequest);

            var marketDepthSubscribeRequest = new MarketDepthSubscribeRequest("btcusdt", MarketDepthStepType.NoAggregation, "id1");
            client.Send(marketDepthSubscribeRequest);

            client.Start();
            
            await Task.Delay(1000);

            var now = new ZonedDateTime(Instant.FromDateTimeOffset(DateTimeOffset.UtcNow), DateTimeZone.Utc);
            var marketCandlestickPullRequest = new MarketCandlestickPullRequest(
                "btcusdt",
                MarketCandlestickPeriodType.SixtyMinutes,
                "id1",
                now.PlusHours(-5),
                now.PlusHours(-2));
            client.Send(marketCandlestickPullRequest);

            var marketDepthPullRequest = new MarketDepthPullRequest(
                "btcusdt",
                MarketDepthStepType.NoAggregation,
                "id1");
            client.Send(marketDepthPullRequest);

            await Task.Delay(1000);
            
            var candlestickUnsubscribeRequest = new MarketCandlestickUnsubscribeRequest("btcusdt", MarketCandlestickPeriodType.OneMinute, "id1");
            client.Send(candlestickUnsubscribeRequest);
            
            var depthUnsubscribeRequest = new MarketDepthUnsubscribeRequest("btcusdt", MarketDepthStepType.NoAggregation, "id1");
            client.Send(depthUnsubscribeRequest);
            
            ExitEvent.WaitOne();
        }
    }
}