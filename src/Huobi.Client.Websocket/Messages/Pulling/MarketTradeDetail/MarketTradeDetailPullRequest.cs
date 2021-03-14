using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Pulling.MarketTradeDetail
{
    public class MarketTradeDetailPullRequest : PullRequest
    {
        public MarketTradeDetailPullRequest(string symbol, string reqId)
            : base(symbol, SubscriptionType.MarketTradeDetail, null, reqId)
        {
        }
    }
}