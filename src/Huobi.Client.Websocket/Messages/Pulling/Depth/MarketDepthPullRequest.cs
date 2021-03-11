using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Pulling.Depth
{
    public class MarketDepthPullRequest : PullRequest
    {
        public MarketDepthPullRequest(string symbol, MarketDepthStepType stepType, string reqId)
            : base(symbol, SubscriptionType.MarketDepth, stepType.ToStep(), reqId)
        {
        }
    }
}