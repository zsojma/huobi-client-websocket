using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPriceRefreshSubscribeRequest : SubscribeRequest
    {
        public MarketByPriceRefreshSubscribeRequest(
            string reqId,
            string symbol,
            int levels)
            : base(reqId, symbol, SubscriptionType.MarketByPriceRefresh, levels.ToString())
        {
        }
    }
}