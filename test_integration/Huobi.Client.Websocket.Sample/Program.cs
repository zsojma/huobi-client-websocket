using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading;
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
        
        private static readonly ManualResetEvent _exitEvent = new(false);
        private static ILogger? _logger;

        public static async Task Main()
        {
            SetupEvents();

            var serviceProvider = SetupServiceProvider();
            _logger = CreateLogger(serviceProvider);

            _logger.LogInformation("Starting application...");

            var examples = serviceProvider.GetRequiredService<IEnumerable<IExample>>().ToArray();
            foreach (var example in examples)
            {
                await example.Start(SYMBOL);
            }
            
            _logger.LogInformation("Press ctrl+c to exit...");
            _exitEvent.WaitOne();
            
            _logger.LogInformation("Stopping application...");

            foreach (var example in examples)
            {
                await example.Stop(SYMBOL);
            }

            // wait until unsubscribe requests are send
            await Task.Delay(1000);
        }

        private static void SetupEvents()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;
        }

        private static ILogger CreateLogger(ServiceProvider serviceProvider)
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
            var marketByPriceClientConfig = configuration.GetSection("HuobiMarketByPriceWebsocketClient");
            var accountClientConfig = configuration.GetSection("HuobiAccountWebsocketClient");

            var serviceCollection = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .AddSingleton(configuration)
                .Configure<HuobiWebsocketClientConfig>(marketClientConfig)
                .Configure<HuobiMarketWebsocketClientConfig>(marketClientConfig)
                .Configure<HuobiMarketByPriceWebsocketClientConfig>(marketByPriceClientConfig)
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
                .AddTransient<IExample, GenericClientExample>()
                .AddTransient<IExample, MarketClientExample>()
                .AddTransient<IExample, MarketByPriceClientExample>()
                .AddTransient<IExample, AccountClientExample>()
                ;
        }

        private static void CurrentDomainOnProcessExit(object? sender, EventArgs eventArgs)
        {
            _logger?.LogWarning("Exiting process");
            _exitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            _logger?.LogWarning("Unloading process");
            _exitEvent.Set();
        }

        private static void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            _logger?.LogWarning("Canceling process");
            e.Cancel = true;
            _exitEvent.Set();
        }
    }
}