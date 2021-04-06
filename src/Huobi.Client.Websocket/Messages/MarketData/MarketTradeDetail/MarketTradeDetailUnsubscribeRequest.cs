using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail
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