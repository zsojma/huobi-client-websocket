using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPricePullRequest : PullRequest
    {
        public MarketByPricePullRequest(
            string reqId,
            string symbol,
            int levels)
            : base(reqId, symbol, SubscriptionType.MarketByPrice, levels.ToString())
        {
        }
    }
}