using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick
{
    public class MarketCandlestickPullRequest : PullRequest
    {
        public MarketCandlestickPullRequest(
            string reqId,
            string symbol,
            MarketCandlestickPeriodType periodType)
            : base(reqId, symbol, SubscriptionType.MarketCandlestick, periodType.ToStep())
        {
        }

        public MarketCandlestickPullRequest(
            string reqId,
            string symbol,
            MarketCandlestickPeriodType periodType,
            DateTimeOffset from,
            DateTimeOffset to)
            : base(reqId, symbol, SubscriptionType.MarketCandlestick, periodType.ToStep())
        {
            From = from;
            To = to;
        }

        [JsonIgnore]
        public DateTimeOffset? From { get; }

        [JsonIgnore]
        public DateTimeOffset? To { get; }

        [JsonProperty("from")]
        public long? FromTick => From?.ToUnixTimeSeconds();

        [JsonProperty("to")]
        public long? ToTick => To?.ToUnixTimeSeconds();
    }
}