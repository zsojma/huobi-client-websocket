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
        private static readonly ManualResetEvent _exitEvent = new(false);

        public static async Task Main()
        {
            var serviceProvider = SetupServiceProvider();
            var logger = SetupLogging(serviceProvider);

            logger.LogInformation("Starting application");

            //var genericClientExecution = serviceProvider.GetRequiredService<GenericClientExample>();
            //await genericClientExecution.Execute("btcusdt");

            //var marketClientExecution = serviceProvider.GetRequiredService<MarketClientExample>();
            //await marketClientExecution.Execute("btcusdt");

            //var authenticationRequestFactory = serviceProvider.GetRequiredService<IHuobiAuthenticationRequestFactory>();

            //var authenticationRequest = authenticationRequestFactory.CreateRequest();
            //client.Send(authenticationRequest);

            //var accountUpdatesSubscribeRequest = new AccountUpdateSubscribeRequest();
            //client.Send(accountUpdatesSubscribeRequest);

            _exitEvent.WaitOne();
        }

        private static ILogger SetupLogging(ServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("SampleApp");
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
                .AddHuobiWebsocketServices()
                .AddTransient<GenericClientExample>()
                .AddTransient<MarketClientExample>()
                .AddTransient<AccountClientExample>();

            var serviceProvider = serviceCollection
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}