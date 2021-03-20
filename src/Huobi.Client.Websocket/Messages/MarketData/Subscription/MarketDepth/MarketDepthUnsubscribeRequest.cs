using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketDepth
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

        public MarketDepthUnsubscribeRequest(
            string symbol,
            string stepType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketDepth, stepType, reqId)
        {
        }
    }
}