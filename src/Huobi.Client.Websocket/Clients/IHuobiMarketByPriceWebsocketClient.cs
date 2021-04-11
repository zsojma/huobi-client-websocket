using Huobi.Client.Websocket.Clients.Streams;
using Huobi.Client.Websocket.Messages.MarketData;

namespace Huobi.Client.Websocket.Clients
{
    public interface IHuobiMarketByPriceWebsocketClient : IHuobiWebsocketClient<HuobiMarketByPriceClientStreams, RequestBase>
    {
    }
}