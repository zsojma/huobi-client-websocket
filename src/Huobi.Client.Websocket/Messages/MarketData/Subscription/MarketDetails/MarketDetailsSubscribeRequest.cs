using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketDetails
{
    public class MarketDetailsSubscribeRequest : SubscribeRequest
    {
        public MarketDetailsSubscribeRequest(string symbol, string? reqId = null)
            : base(symbol, SubscriptionType.MarketDetails, null, reqId)
        {
        }
    }
}