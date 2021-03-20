using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Ticks
{
    public class MarketTradeDetailTick
    {
        [JsonConstructor]
        public MarketTradeDetailTick(long id, long timestamp, MarketTradeDetailTickDataItem[] data)
        {
            Id = id;
            Timestamp = timestamp;
            Data = data;
        }

        public long Id { get; }

        [JsonProperty("ts")]
        public long Timestamp { get; }

        public MarketTradeDetailTickDataItem[] Data { get; }
    }
}