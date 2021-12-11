using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;

public class MarketTradeDetailPullRequest : PullRequest
{
    public MarketTradeDetailPullRequest(string reqId, string symbol)
        : base(reqId, symbol, SubscriptionType.MarketTradeDetail)
    {
    }
}