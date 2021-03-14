using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketTradeDetail
{
    public class MarketTradeDetailUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketTradeDetailUnsubscribeRequest(
            string symbol,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketTradeDetail, null, reqId)
        {
        }
    }
}