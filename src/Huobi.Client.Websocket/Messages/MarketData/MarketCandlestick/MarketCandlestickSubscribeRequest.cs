using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick
{
    public class MarketCandlestickSubscribeRequest : SubscribeRequest
    {
        public MarketCandlestickSubscribeRequest(
            string reqId,
            string symbol,
            MarketCandlestickPeriodType periodType)
            : base(reqId, symbol, SubscriptionType.MarketCandlestick, periodType.ToStep())
        {
        }
    }
}