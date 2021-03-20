using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketDepth
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