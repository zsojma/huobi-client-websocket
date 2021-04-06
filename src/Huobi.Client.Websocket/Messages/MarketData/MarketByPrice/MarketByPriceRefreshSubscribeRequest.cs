using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPriceRefreshSubscribeRequest : SubscribeRequest
    {
        public MarketByPriceRefreshSubscribeRequest(
            string symbol,
            MarketByPriceRefreshLevelType levelType,
            string reqId)
            : base(symbol, SubscriptionType.MarketByPriceRefresh, levelType.ToStep(), reqId)
        {
        }

        public MarketByPriceRefreshSubscribeRequest(
            string symbol,
            string levelType,
            string reqId)
            : base(symbol, SubscriptionType.MarketByPriceRefresh, levelType, reqId)
        {
        }
    }
}