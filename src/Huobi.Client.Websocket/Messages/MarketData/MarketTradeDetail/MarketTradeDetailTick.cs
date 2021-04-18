using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail
{
    public class MarketTradeDetailTick
    {
        [JsonConstructor]
        public MarketTradeDetailTick(long id, DateTimeOffset timestamp, MarketTradeDetailTickDataItem[] data)
        {
            Validations.ValidateInput(data, nameof(data));

            Id = id;
            Timestamp = timestamp;
            Data = data;
        }

        public long Id { get; }

        [JsonProperty("ts")]
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; }
        
        public MarketTradeDetailTickDataItem[] Data { get; }
    }
}