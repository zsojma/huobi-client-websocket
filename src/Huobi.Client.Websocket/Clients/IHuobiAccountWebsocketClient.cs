using Huobi.Client.Websocket.Clients.Streams;
using Huobi.Client.Websocket.Messages.Account;

namespace Huobi.Client.Websocket.Clients
{
    public interface IHuobiAccountWebsocketClient : IHuobiWebsocketClient<HuobiAccountClientStreams, AccountRequestBase>
    {
    }
}