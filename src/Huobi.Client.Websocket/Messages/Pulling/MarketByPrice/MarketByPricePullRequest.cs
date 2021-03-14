using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Pulling.MarketByPrice
{
    public class MarketByPricePullRequest : PullRequest
    {
        public MarketByPricePullRequest(
            string symbol,
            MarketByPriceLevelType levelType,
            string reqId)
            : base(symbol, SubscriptionType.MarketByPrice, levelType.ToStep(), reqId)
        {
        }

        public MarketByPricePullRequest(
            string symbol,
            string levelType,
            string reqId)
            : base(symbol, SubscriptionType.MarketByPrice, levelType, reqId)
        {
        }
    }
}