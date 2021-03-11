using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.Candlestick
{
    public class MarketCandlestickSubscribeRequest : SubscribeRequest
    {
        public MarketCandlestickSubscribeRequest(
            string symbol,
            MarketCandlestickPeriodType periodType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType.ToStep(), reqId)
        {
        }
    }
}