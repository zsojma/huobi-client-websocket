using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class UpdateMessage<TTick> : IResponse
        where TTick : class
    {
        [JsonConstructor]
        public UpdateMessage(string topic, DateTimeOffset timestamp, TTick tick)
        {
            Validations.ValidateInput(topic, nameof(topic));
            Validations.ValidateInput(tick, nameof(tick));

            Topic = topic;
            Timestamp = timestamp;
            Tick = tick;
        }

        [JsonProperty("ch")]
        public string Topic { get; }

        [JsonProperty("ts")]
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; }

        public TTick Tick { get; }
    }
}