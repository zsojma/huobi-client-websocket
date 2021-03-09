using System;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Messages.Subscription;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            client.Streams.UnhandledMessageStream.Subscribe(x => { logger.LogInformation($"Unhandled message: {x}"); });
            
            client.Streams.MarketCandlestickStream.Subscribe(
                x =>
                {
                    logger.LogInformation($"Candle {x.Channel} | [amount={x.Tick.Amount}] [open={x.Tick.Open}] [close={x.Tick.Close}] [low={x.Tick.Low}] [high={x.Tick.High}]");
                });
            
            client.Streams.MarketDepthStream.Subscribe(
                x =>
                {
                    var i = 0;
                    while (i < 10 && i < x.Tick.Bids.Length)
                    {
                        var bid = x.Tick.Bids[i];
                        var ask = x.Tick.Asks[i];

                        ++i;

                        logger.LogInformation($"Depth {x.Channel} | [bid {i}: price={bid[0]} size={bid[1]}] [ask {i}: price={ask[0]} size={ask[1]}]");
                    }
                });

            var request = new SubscribeRequest("btcusdt", SubscriptionType.MarketCandlestick, "1min", "id1");
            //var request = new SubscribeRequest("btcusdt", SubscriptionType.MarketDepth, "step0", "id1");
            client.Send(request);

            client.Start();
            
            ExitEvent.WaitOne();
        }
    }
}