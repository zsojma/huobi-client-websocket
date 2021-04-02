using Huobi.Client.Websocket.Clients.Streams;

namespace Huobi.Client.Websocket.Clients
{
    public interface IHuobiGenericWebsocketClient : IHuobiWebsocketClient<HuobiGenericClientStreams, object>
    {
    }
}