using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class PullResponse<TTick> : IResponse
        where TTick : class
    {
        public PullResponse(string? reqId, string? status, string? topic, DateTimeOffset timestamp, TTick? data)
        {
            ReqId = reqId ?? string.Empty;
            Status = status ?? string.Empty;
            Topic = topic ?? string.Empty;
            Timestamp = timestamp;
            Data = data;
        }

        [JsonProperty("id")]
        public string ReqId { get; }

        public string Status { get; }

        [JsonProperty("rep")]
        public string Topic { get; }

        [JsonProperty("ts")]
        public DateTimeOffset Timestamp { get; }

        public TTick? Data { get; }
    }
}