using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;

public class MarketByPriceUnsubscribeRequest : UnsubscribeRequest
{
    public MarketByPriceUnsubscribeRequest(
        string reqId,
        string symbol,
        int levels)
        : base(reqId, symbol, SubscriptionType.MarketByPrice, levels.ToString())
    {
    }
}