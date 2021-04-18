using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class UnsubscribeResponse : IResponse
    {
        [JsonConstructor]
        public UnsubscribeResponse(string reqId, string status, string topic, DateTimeOffset timestamp)
        {
            Validations.ValidateInput(reqId, nameof(reqId));
            Validations.ValidateInput(status, nameof(status));
            Validations.ValidateInput(topic, nameof(topic));

            ReqId = reqId;
            Status = status;
            Topic = topic;
            Timestamp = timestamp;
        }
        
        [JsonProperty("id")]
        public string ReqId { get; }

        public string Status { get; }

        [JsonProperty("unsubbed")]
        public string Topic { get; }

        [JsonProperty("ts")]
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out UnsubscribeResponse response)
        {
            var result = serializer.TryDeserializeIfContains(input, "\"unsubbed\"", out response);
            return result && response?.Timestamp.Ticks > 0;
        }
    }
}