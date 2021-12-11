using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDetails;

public class MarketDetailsUnsubscribeRequest : UnsubscribeRequest
{
    public MarketDetailsUnsubscribeRequest(string reqId, string symbol)
        : base(reqId, symbol, SubscriptionType.MarketDetails)
    {
    }
}