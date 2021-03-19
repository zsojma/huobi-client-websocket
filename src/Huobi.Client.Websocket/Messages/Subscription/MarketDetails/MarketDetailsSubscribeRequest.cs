using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketDetails
{
    public class MarketDetailsSubscribeRequest : SubscribeRequest
    {
        public MarketDetailsSubscribeRequest(string symbol, string? reqId = null)
            : base(symbol, SubscriptionType.MarketDetails, null, reqId)
        {
        }
    }
}