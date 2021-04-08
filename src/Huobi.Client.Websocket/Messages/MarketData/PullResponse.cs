using System;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class PullResponse<TTick> : ResponseBase
        where TTick : class
    {
        public PullResponse(string reqId, string status, string topic, long timestampMs, TTick data)
            : base(reqId)
        {
            Validations.ValidateInput(status, nameof(status));
            Validations.ValidateInput(topic, nameof(topic));
            Validations.ValidateInput(data, nameof(data));

            Status = status;
            Topic = topic;
            TimestampMs = timestampMs;
            Data = data;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        public string Status { get; }

        [JsonProperty("rep")]
        public string Topic { get; }

        public TTick Data { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }
    }
}