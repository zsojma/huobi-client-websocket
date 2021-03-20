using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;
using NodaTime;

namespace Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketCandlestick
{
    public class MarketCandlestickPullRequest : PullRequest
    {
        public MarketCandlestickPullRequest(
            string symbol,
            MarketCandlestickPeriodType periodType,
            string reqId)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType.ToStep(), reqId)
        {
        }

        public MarketCandlestickPullRequest(
            string symbol,
            string periodType,
            string reqId)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType, reqId)
        {
        }

        public MarketCandlestickPullRequest(
            string symbol,
            MarketCandlestickPeriodType periodType,
            string reqId,
            ZonedDateTime from,
            ZonedDateTime to)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType.ToStep(), reqId)
        {
            From = from;
            To = to;
        }

        public MarketCandlestickPullRequest(
            string symbol,
            string periodType,
            string reqId,
            ZonedDateTime from,
            ZonedDateTime to)
            : base(symbol, SubscriptionType.MarketCandlestick, periodType, reqId)
        {
            From = from;
            To = to;
        }

        [JsonIgnore]
        public ZonedDateTime? From { get; }

        [JsonIgnore]
        public ZonedDateTime? To { get; }

        [JsonProperty("from")]
        public long? FromTick => From?.ToDateTimeOffset().ToUnixTimeSeconds();

        [JsonProperty("to")]
        public long? ToTick => To?.ToDateTimeOffset().ToUnixTimeSeconds();
    }
}