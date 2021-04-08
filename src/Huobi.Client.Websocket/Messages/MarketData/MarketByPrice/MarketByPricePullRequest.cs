using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPricePullRequest : PullRequest
    {
        public MarketByPricePullRequest(
            string reqId,
            string symbol,
            MarketByPriceLevelType levelType)
            : base(reqId, symbol, SubscriptionType.MarketByPrice, levelType.ToStep())
        {
        }
    }
}