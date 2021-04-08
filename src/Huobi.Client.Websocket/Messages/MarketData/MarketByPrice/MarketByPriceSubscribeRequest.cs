using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPriceSubscribeRequest : SubscribeRequest
    {
        public MarketByPriceSubscribeRequest(
            string reqId,
            string symbol,
            MarketByPriceLevelType levelType)
            : base(reqId, symbol, SubscriptionType.MarketByPrice, levelType.ToStep())
        {
        }
    }
}