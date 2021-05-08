using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDepth
{
    public class MarketDepthUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketDepthUnsubscribeRequest(string reqId, string symbol, int stepIndex)
            : base(reqId, symbol, SubscriptionType.MarketDepth, $"step{stepIndex}")
        {
        }
    }
}