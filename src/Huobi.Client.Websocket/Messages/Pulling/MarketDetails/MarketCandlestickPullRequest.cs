using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Pulling.MarketDetails
{
    public class MarketDetailsPullRequest : PullRequest
    {
        public MarketDetailsPullRequest(string symbol, string reqId)
            : base(symbol, SubscriptionType.MarketDetails, null, reqId)
        {
        }
    }
}