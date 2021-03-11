using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.Depth
{
    public class MarketDepthSubscribeRequest : SubscribeRequest
    {
        public MarketDepthSubscribeRequest(
            string symbol,
            MarketDepthStepType stepType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketDepth, stepType.ToStep(), reqId)
        {
        }
    }
}