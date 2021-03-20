using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketByPrice
{
    public class MarketByPriceSubscribeRequest : SubscribeRequest
    {
        public MarketByPriceSubscribeRequest(
            string symbol,
            MarketByPriceLevelType levelType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketByPrice, levelType.ToStep(), reqId)
        {
        }

        public MarketByPriceSubscribeRequest(
            string symbol,
            string levelType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketByPrice, levelType, reqId)
        {
        }
    }
}