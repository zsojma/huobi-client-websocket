using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPriceRefreshUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketByPriceRefreshUnsubscribeRequest(
            string reqId,
            string symbol,
            MarketByPriceRefreshLevelType levelType)
            : base(reqId, symbol, SubscriptionType.MarketByPriceRefresh, levelType.ToStep())
        {
        }
    }
}