using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.Depth
{
    public class MarketDepthUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketDepthUnsubscribeRequest(
            string symbol,
            MarketDepthStepType stepType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketDepth, stepType.ToStep(), reqId)
        {
        }
    }
}