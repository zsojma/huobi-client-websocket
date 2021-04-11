using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail
{
    public class MarketTradeDetailTickDataItem
    {
        [JsonConstructor]
        public MarketTradeDetailTickDataItem(
            string id,
            long timestampMs,
            decimal amount,
            long tradeId,
            decimal price,
            TradeSide direction)
        {
            Id = id;
            TimestampMs = timestampMs;
            Amount = amount;
            TradeId = tradeId;
            Price = price;
            Direction = direction;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        public string Id { get; }
        public decimal Amount { get; }
        public long TradeId { get; }
        public decimal Price { get; }
        public TradeSide Direction { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }
    }
}