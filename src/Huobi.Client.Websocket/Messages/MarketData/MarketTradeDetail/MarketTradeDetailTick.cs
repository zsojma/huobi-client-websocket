using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail
{
    public class MarketTradeDetailTick
    {
        [JsonConstructor]
        public MarketTradeDetailTick(long id, DateTimeOffset timestamp, MarketTradeDetailTickDataItem[]? data)
        {
            Id = id;
            Timestamp = timestamp;
            Data = data ?? Array.Empty<MarketTradeDetailTickDataItem>();
        }

        public long Id { get; }

        [JsonProperty("ts")]
        public DateTimeOffset Timestamp { get; }
        
        public MarketTradeDetailTickDataItem[] Data { get; }
    }
}