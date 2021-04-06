using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick
{
    public class MarketCandlestickUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketCandlestickUnsubscribeRequest(
            string symbol,
            MarketCandlestickPeriodType periodType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType.ToStep(), reqId)
        {
        }

        public MarketCandlestickUnsubscribeRequest(
            string symbol,
            string periodType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType, reqId)
        {
        }
    }
}