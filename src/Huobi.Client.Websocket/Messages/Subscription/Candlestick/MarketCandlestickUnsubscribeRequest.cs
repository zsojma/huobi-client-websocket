using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.Candlestick
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
    }
}