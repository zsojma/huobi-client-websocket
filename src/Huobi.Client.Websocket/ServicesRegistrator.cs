using System;
using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket
{
    public static class ServicesRegistrator
    {
        public static IServiceCollection AddHuobiWebsocketServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IHuobiSerializer, HuobiSerializer>();
            serviceCollection.AddTransient<IHuobiDateTimeProvider, HuobiDateTimeProvider>();

            serviceCollection.AddTransient<IHuobiSignature, HuobiSignature>();
            serviceCollection.AddTransient<IHuobiAuthenticationRequestFactory, HuobiAuthenticationRequestFactory>();

            serviceCollection.AddTransient(CreateGenericClient);
            serviceCollection.AddTransient(CreateMarketClient);
            serviceCollection.AddTransient(CreateMarketByPriceClient);
            serviceCollection.AddTransient(CreateAccountClient);

            return serviceCollection;
        }

        private static IHuobiGenericWebsocketClient CreateGenericClient(IServiceProvider provider)
        {
            var config = provider.GetRequiredService<IOptions<HuobiWebsocketClientConfig>>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            return HuobiWebsocketClientsFactory.CreateGenericClient(config.Value, loggerFactory);
        }

        private static IHuobiMarketWebsocketClient CreateMarketClient(IServiceProvider provider)
        {
            var config = provider.GetRequiredService<IOptions<HuobiMarketWebsocketClientConfig>>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            return HuobiWebsocketClientsFactory.CreateMarketClient(config.Value, loggerFactory);
        }

        private static IHuobiMarketByPriceWebsocketClient CreateMarketByPriceClient(IServiceProvider provider)
        {
            var config = provider.GetRequiredService<IOptions<HuobiMarketByPriceWebsocketClientConfig>>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            return HuobiWebsocketClientsFactory.CreateMarketByPriceClient(config.Value, loggerFactory);
        }

        private static IHuobiAccountWebsocketClient CreateAccountClient(IServiceProvider provider)
        {
            var config = provider.GetRequiredService<IOptions<HuobiAccountWebsocketClientConfig>>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            return HuobiWebsocketClientsFactory.CreateAccountClient(config.Value, loggerFactory);
        }
    }
}