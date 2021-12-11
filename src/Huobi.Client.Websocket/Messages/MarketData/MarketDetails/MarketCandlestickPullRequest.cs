using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDetails;

public class MarketDetailsPullRequest : PullRequest
{
    public MarketDetailsPullRequest(string reqId, string symbol)
        : base(reqId, symbol, SubscriptionType.MarketDetails)
    {
    }
}