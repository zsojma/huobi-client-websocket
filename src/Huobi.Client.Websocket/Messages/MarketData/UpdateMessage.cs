using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class UpdateMessage<TTick> : IResponse
        where TTick : class
    {
        [JsonConstructor]
        public UpdateMessage(string? topic, DateTimeOffset timestamp, TTick? tick)
        {
            Topic = topic ?? string.Empty;
            Timestamp = timestamp;
            Tick = tick;
        }

        [JsonProperty("ch")]
        public string Topic { get; }

        [JsonProperty("ts")]
        public DateTimeOffset Timestamp { get; }

        public TTick? Tick { get; }
    }
}