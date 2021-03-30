using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages.Account.Factories;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Huobi.Client.Websocket
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddHuobiWebsocketServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IHuobiSerializer, HuobiSerializer>();
            serviceCollection.AddTransient<IHuobiDateTimeProvider, HuobiDateTimeProvider>();

            serviceCollection.AddTransient<IHuobiAuthentication, HuobiAuthentication>();
            serviceCollection.AddTransient<IHuobiAuthenticationRequestFactory, HuobiAuthenticationRequestFactory>();
            
            serviceCollection.AddTransient<IHuobiWebsocketCommunicator, HuobiWebsocketCommunicator>();
            serviceCollection.AddTransient<IHuobiWebsocketClient, HuobiWebsocketClient>();
            
            return serviceCollection;
        }
    }
}
