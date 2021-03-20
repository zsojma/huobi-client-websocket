using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketByPrice
{
    public class MarketByPriceUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketByPriceUnsubscribeRequest(
            string symbol,
            MarketByPriceLevelType levelType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketByPrice, levelType.ToStep(), reqId)
        {
        }

        public MarketByPriceUnsubscribeRequest(
            string symbol,
            string levelType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketByPrice, levelType, reqId)
        {
        }
    }
}