using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Pulling.MarketDepth
{
    public class MarketDepthPullRequest : PullRequest
    {
        public MarketDepthPullRequest(string symbol, MarketDepthStepType stepType, string reqId)
            : base(symbol, SubscriptionType.MarketDepth, stepType.ToStep(), reqId)
        {
        }

        public MarketDepthPullRequest(string symbol, string stepType, string reqId)
            : base(symbol, SubscriptionType.MarketDepth, stepType, reqId)
        {
        }
    }
}