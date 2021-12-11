using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;

public class MarketCandlestickUnsubscribeRequest : UnsubscribeRequest
{
    public MarketCandlestickUnsubscribeRequest(
        string reqId,
        string symbol,
        MarketCandlestickPeriodType periodType)
        : base(reqId, symbol, SubscriptionType.MarketCandlestick, periodType.ToStep())
    {
    }
}