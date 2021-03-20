using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketTradeDetail
{
    public class MarketTradeDetailPullRequest : PullRequest
    {
        public MarketTradeDetailPullRequest(string symbol, string reqId)
            : base(symbol, SubscriptionType.MarketTradeDetail, null, reqId)
        {
        }
    }
}