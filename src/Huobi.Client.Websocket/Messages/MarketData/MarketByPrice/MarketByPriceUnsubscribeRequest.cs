using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPriceUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketByPriceUnsubscribeRequest(
            string reqId,
            string symbol,
            MarketByPriceLevelType levelType)
            : base(reqId, symbol, SubscriptionType.MarketByPrice, levelType.ToStep())
        {
        }
    }
}