using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketTradeDetail
{
    public class MarketTradeDetailSubscribeRequest : SubscribeRequest
    {
        public MarketTradeDetailSubscribeRequest(
            string symbol,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketTradeDetail, null, reqId)
        {
        }
    }
}