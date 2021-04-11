using System;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail
{
    public class MarketTradeDetailTick
    {
        [JsonConstructor]
        public MarketTradeDetailTick(long id, long timestampMs, MarketTradeDetailTickDataItem[] data)
        {
            Validations.ValidateInput(data, nameof(data));

            Id = id;
            TimestampMs = timestampMs;
            Data = data;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        public long Id { get; }
        public MarketTradeDetailTickDataItem[] Data { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }
    }
}