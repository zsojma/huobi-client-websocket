using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDetails;

public class MarketDetailsSubscribeRequest : SubscribeRequest
{
    public MarketDetailsSubscribeRequest(string reqId, string symbol)
        : base(reqId, symbol, SubscriptionType.MarketDetails)
    {
    }
}