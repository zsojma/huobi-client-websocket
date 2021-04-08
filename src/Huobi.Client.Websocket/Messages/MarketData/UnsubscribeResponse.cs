using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class UnsubscribeResponse : ResponseBase
    {
        [JsonConstructor]
        public UnsubscribeResponse(string reqId, string status, string topic, long timestampMs)
            : base(reqId)
        {
            Validations.ValidateInput(status, nameof(status));
            Validations.ValidateInput(topic, nameof(topic));

            Status = status;
            Topic = topic;
            TimestampMs = timestampMs;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        public string Status { get; }

        [JsonProperty("unsubbed")]
        public string Topic { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out UnsubscribeResponse response)
        {
            var result = serializer.TryDeserializeIfContains(input, "\"unsubbed\"", out response);
            return result && response?.TimestampMs > 0;
        }
    }
}