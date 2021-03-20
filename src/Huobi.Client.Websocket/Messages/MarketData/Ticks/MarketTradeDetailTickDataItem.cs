using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Ticks
{
    public class MarketTradeDetailTickDataItem
    {
        [JsonConstructor]
        public MarketTradeDetailTickDataItem(
            decimal amount,
            long timestamp,
            string id,
            long tradeId,
            decimal price,
            string direction)
        {
            Amount = amount;
            Timestamp = timestamp;
            Id = id;
            TradeId = tradeId;
            Price = price;
            Direction = direction;
        }

        public decimal Amount { get; }

        [JsonProperty("ts")]
        public long Timestamp { get; }

        public string Id { get; }
        public long TradeId { get; }
        public decimal Price { get; }
        public string Direction { get; }
    }
}