using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketCandlestick
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

        public MarketCandlestickSubscribeRequest(
            string symbol,
            string periodType,
            string? reqId = null)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType, reqId)
        {
        }
    }
}