using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;

public class MarketTradeDetailUnsubscribeRequest : UnsubscribeRequest
{
    public MarketTradeDetailUnsubscribeRequest(
        string reqId,
        string symbol)
        : base(reqId, symbol, SubscriptionType.MarketTradeDetail)
    {
    }
}