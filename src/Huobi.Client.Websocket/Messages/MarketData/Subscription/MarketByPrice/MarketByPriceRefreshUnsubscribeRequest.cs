using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketByPrice
{
    public class MarketByPriceRefreshUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketByPriceRefreshUnsubscribeRequest(
            string symbol,
            MarketByPriceRefreshLevelType levelType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketByPriceRefresh, levelType.ToStep(), reqId)
        {
        }

        public MarketByPriceRefreshUnsubscribeRequest(
            string symbol,
            string levelType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketByPriceRefresh, levelType, reqId)
        {
        }
    }
}