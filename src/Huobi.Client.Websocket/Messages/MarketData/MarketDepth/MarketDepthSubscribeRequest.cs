using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDepth;

public class MarketDepthSubscribeRequest : SubscribeRequest
{
    public MarketDepthSubscribeRequest(string reqId, string symbol, int stepIndex)
        : base(reqId, symbol, SubscriptionType.MarketDepth, $"step{stepIndex}")
    {
    }
}