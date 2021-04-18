using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class PullResponse<TTick> : IResponse
        where TTick : class
    {
        public PullResponse(string reqId, string status, string topic, DateTimeOffset timestamp, TTick data)
        {
            Validations.ValidateInput(reqId, nameof(reqId));
            Validations.ValidateInput(status, nameof(status));
            Validations.ValidateInput(topic, nameof(topic));
            Validations.ValidateInput(data, nameof(data));

            ReqId = reqId;
            Status = status;
            Topic = topic;
            Timestamp = timestamp;
            Data = data;
        }

        [JsonProperty("id")]
        public string ReqId { get; }

        public string Status { get; }

        [JsonProperty("rep")]
        public string Topic { get; }

        public TTick Data { get; }

        [JsonProperty("ts")]
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; }
    }
}