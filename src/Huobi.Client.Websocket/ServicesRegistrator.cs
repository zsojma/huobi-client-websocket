using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Microsoft.Extensions.DependencyInjection;

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

            serviceCollection.AddTransient<IHuobiGenericWebsocketCommunicator, HuobiGenericWebsocketCommunicator>();
            serviceCollection.AddTransient<IHuobiMarketWebsocketCommunicator, HuobiMarketWebsocketCommunicator>();
            serviceCollection.AddTransient<IHuobiMarketByPriceWebsocketCommunicator, HuobiMarketByPriceWebsocketCommunicator>();
            serviceCollection.AddTransient<IHuobiAccountWebsocketCommunicator, HuobiAccountWebsocketCommunicator>();

            serviceCollection.AddTransient<IHuobiGenericWebsocketClient, HuobiGenericWebsocketClient>();
            serviceCollection.AddTransient<IHuobiMarketWebsocketClient, HuobiMarketWebsocketClient>();
            serviceCollection.AddTransient<IHuobiMarketByPriceWebsocketClient, HuobiMarketByPriceWebsocketClient>();
            serviceCollection.AddTransient<IHuobiAccountWebsocketClient, HuobiAccountWebsocketClient>();

            return serviceCollection;
        }
    }
}