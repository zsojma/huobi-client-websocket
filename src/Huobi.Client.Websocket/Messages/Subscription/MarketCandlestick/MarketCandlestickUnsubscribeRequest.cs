using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketCandlestick
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