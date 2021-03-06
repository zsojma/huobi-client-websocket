using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Communicator;
using Microsoft.Extensions.DependencyInjection;

namespace Huobi.Client.Websocket
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddHuobiWebsocketServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IHuobiWebsocketCommunicator, HuobiWebsocketCommunicator>();
            serviceCollection.AddTransient<IHuobiWebsocketClient, HuobiWebsocketClient>();
            return serviceCollection;
        }
    }
}
