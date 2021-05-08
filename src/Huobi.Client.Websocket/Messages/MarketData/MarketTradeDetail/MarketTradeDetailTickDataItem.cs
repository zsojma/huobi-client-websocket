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
            decimal amount,
            long tradeId,
            decimal price,
            DateTimeOffset timestamp,
            TradeSide direction)
        {
            Id = id;
            Amount = amount;
            TradeId = tradeId;
            Price = price;
            Timestamp = timestamp;
            Direction = direction;
        }

        public string Id { get; }
        public decimal Amount { get; }
        public long TradeId { get; }
        public decimal Price { get; }

        [JsonProperty("ts")]
        public DateTimeOffset Timestamp { get; }

        public TradeSide Direction { get; }
    }
}