using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketDetails
{
    public class MarketDetailsUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketDetailsUnsubscribeRequest(string symbol, string? reqId = null)
            : base(symbol, SubscriptionType.MarketDetails, null, reqId)
        {
        }
    }
}