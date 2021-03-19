using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketDetails
{
    public class MarketDetailsUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketDetailsUnsubscribeRequest(string symbol, string? reqId = null)
            : base(symbol, SubscriptionType.MarketDetails, null, reqId)
        {
        }
    }
}