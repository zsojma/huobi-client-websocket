using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDepth
{
    public class MarketDepthPullRequest : PullRequest
    {
        public MarketDepthPullRequest(string reqId, string symbol, MarketDepthStepType stepType)
            : base(reqId, symbol, SubscriptionType.MarketDepth, stepType.ToStep())
        {
        }
    }
}