using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Sample.Examples;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Huobi.Client.Websocket.Sample
{
    public static class Setup
    {
        private static IServiceProvider? _serviceProvider;
        private static ManualResetEvent? _exitEvent;
        private static ILogger? _logger;

        public static IServiceProvider ServiceProvider => _serviceProvider ??= CreateServiceProvider();
        public static ManualResetEvent ExitEvent => _exitEvent ??= CreateExitEvent();
        public static ILogger Logger => _logger ??= CreateLogger(ServiceProvider);

        private static void SetupExamples(IServiceCollection serviceCollection)
        {
            // Comment out services for which you don't want to run the example:

            serviceCollection
                // .AddTransient<IExample, GenericClientExample>()
                //.AddTransient<IExample, MarketClientExample>()
                // .AddTransient<IExample, AccountClientExample>()
                .AddTransient<IExample, FuturesClientExample>()
                ;
        }

        private static IServiceProvider CreateServiceProvider()
        {
            CreateExitEvent();

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.dev.json", true);

            var configuration = configurationBuilder.Build();
            //var genericClientConfig = configuration.GetSection("HuobiGenericWebsocketClient");
            //var marketClientConfig = configuration.GetSection("HuobiMarketWebsocketClient");
            //var marketByPriceClientConfig = configuration.GetSection("HuobiMarketByPriceWebsocketClient");
            //var accountClientConfig = configuration.GetSection("HuobiAccountWebsocketClient");
            var futuresClientConfig = configuration.GetSection("HuobiFuturesAccountWebsocketClient");
            var futuresClientConfigPerp = configuration.GetSection("HuobiPerpFuturesAccountWebsocketClient");
            //var futuresPerpClientConfig = configuration.GetSection("HuobiPerpFuturesAccountWebsocketClient");

            var serviceCollection = new ServiceCollection()
                .AddSingleton(configuration)
                .AddHuobiLogging()
                //.Configure<HuobiGenericWebsocketClientConfig>(genericClientConfig)
                //.Configure<HuobiMarketWebsocketClientConfig>(marketClientConfig)
                //.Configure<HuobiMarketByPriceWebsocketClientConfig>(marketByPriceClientConfig)
                // .Configure<HuobiAccountWebsocketClientConfig>(accountClientConfig)//accountClientConfig
                .Configure<HuobiAccountWebsocketClientConfig>(futuresClientConfigPerp)//accountClientConfig
                //.Configure<HuobiFuturesAccountWebsocketClient>(futuresClientConfig)
                //.Configure<HuobiPerpFuturesAccountWebsocketClient>(futuresPerpClientConfig)
                .AddHuobiWebsocketServices();

            SetupExamples(serviceCollection);

            var serviceProvider = serviceCollection
                .BuildServiceProvider();
            return serviceProvider;
        }

        private static IServiceCollection AddHuobiLogging(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(
                builder =>
                {
                    var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
                    var logPath = Path.Combine(executingDir!, "logs", "verbose.log");
                    var logger = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                        .WriteTo.Console(
                            LogEventLevel.Debug,
                            "{Timestamp:HH:mm:ss.ffffff} [{Level:u3}] {Message}{NewLine}")
                        .CreateLogger();

                    builder.AddSerilog(logger);
                });

            return serviceCollection;
        }

        private static ManualResetEvent CreateExitEvent()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;

            return new ManualResetEvent(false);
        }

        private static void CurrentDomainOnProcessExit(object? sender, EventArgs eventArgs)
        {
            _logger?.LogWarning("Exiting process");
            ExitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            _logger?.LogWarning("Unloading process");
            ExitEvent.Set();
        }

        private static void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            _logger?.LogWarning("Canceling process");
            e.Cancel = true;
            ExitEvent.Set();
        }

        private static ILogger CreateLogger(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("HuobiSampleApp");
            return logger;
        }
    }
}