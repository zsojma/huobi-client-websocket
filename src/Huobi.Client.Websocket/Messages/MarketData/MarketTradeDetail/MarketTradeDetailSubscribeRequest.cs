using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail
{
    public class MarketTradeDetailSubscribeRequest : SubscribeRequest
    {
        public MarketTradeDetailSubscribeRequest(
            string reqId,
            string symbol)
            : base(reqId, symbol, SubscriptionType.MarketTradeDetail)
        {
        }
    }
}