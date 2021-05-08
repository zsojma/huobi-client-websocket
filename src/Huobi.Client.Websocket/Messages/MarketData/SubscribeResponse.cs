using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class SubscribeResponse
    {
        [JsonConstructor]
        public SubscribeResponse(string? reqId, string? status, string? topic, DateTimeOffset timestamp)
        {
            ReqId = reqId ?? string.Empty;
            Status = status ?? string.Empty;
            Topic = topic ?? string.Empty;
            Timestamp = timestamp;
        }

        [JsonProperty("id")]
        public string ReqId { get; }

        public string Status { get; }

        [JsonProperty("subbed")]
        public string Topic { get; }

        [JsonProperty("ts")]
        public DateTimeOffset Timestamp { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out SubscribeResponse response)
        {
            var result = serializer.TryDeserializeIfContains(input, "\"subbed\"", out response);
            return result && response?.Timestamp.Ticks > 0;
        }
    }
}