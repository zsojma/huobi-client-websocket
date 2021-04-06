using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDetails
{
    public class MarketDetailsPullRequest : PullRequest
    {
        public MarketDetailsPullRequest(string symbol, string reqId)
            : base(symbol, SubscriptionType.MarketDetails, null, reqId)
        {
        }
    }
}