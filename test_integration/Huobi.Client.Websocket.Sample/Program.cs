using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Sample.Examples;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Sample
{
    public static class Program
    {
        private const string SYMBOL = "btcusdt";

        public static async Task Main()
        {
            var serviceProvider = SetupServiceProvider();
            var logger = SetupLogging(serviceProvider);

            logger.LogInformation("Starting application...");

            var examples = serviceProvider.GetRequiredService<IEnumerable<IExample>>().ToArray();
            foreach (var example in examples)
            {
                await example.Start(SYMBOL);
            }
            
            logger.LogInformation("Press any key to exit...");
            Console.ReadKey();
            
            logger.LogInformation("Stopping application...");

            foreach (var example in examples)
            {
                await example.Stop(SYMBOL);
            }

            // wait until unsubscribe requests are send
            await Task.Delay(1000);
        }

        private static ILogger SetupLogging(ServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("HuobiSampleApp");
            return logger;
        }

        private static ServiceProvider SetupServiceProvider()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.dev.json", true);

            var configuration = configurationBuilder.Build();
            var marketClientConfig = configuration.GetSection("HuobiMarketWebsocketClient");
            var accountClientConfig = configuration.GetSection("HuobiAccountWebsocketClient");

            var serviceCollection = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .AddSingleton(configuration)
                .Configure<HuobiWebsocketClientConfig>(marketClientConfig)
                .Configure<HuobiAccountWebsocketClientConfig>(accountClientConfig)
                .AddHuobiWebsocketServices();

            SetupExamples(serviceCollection);

            var serviceProvider = serviceCollection
                .BuildServiceProvider();
            return serviceProvider;
        }

        private static void SetupExamples(IServiceCollection serviceCollection)
        {
            // Comment out service for which you want to run the example:

            serviceCollection
                //.AddTransient<IExample, GenericClientExample>()
                .AddTransient<IExample, MarketClientExample>()
                //.AddTransient<IExample, AccountClientExample>()
                ;
        }
    }
}