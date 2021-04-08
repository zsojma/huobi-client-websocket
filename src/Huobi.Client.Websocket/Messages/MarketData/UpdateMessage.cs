using System;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class UpdateMessage<TTick>
        where TTick : class
    {
        [JsonConstructor]
        public UpdateMessage(string topic, long timestampMs, TTick tick)
        {
            Validations.ValidateInput(topic, nameof(topic));
            Validations.ValidateInput(tick, nameof(tick));

            Topic = topic;
            TimestampMs = timestampMs;
            Tick = tick;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        [JsonProperty("ch")]
        public string Topic { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }

        public TTick Tick { get; }
    }
}