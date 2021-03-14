using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketDepth
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

        public MarketDepthSubscribeRequest(
            string symbol,
            string stepType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketDepth, stepType, reqId)
        {
        }
    }
}