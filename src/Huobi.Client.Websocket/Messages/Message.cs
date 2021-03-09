using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public class Message<TTick>
        where TTick : class
    {
        [JsonConstructor]
        public Message(string channel, long timestamp, TTick tick)
        {
            Channel = channel;
            Timestamp = timestamp;
            Tick = tick;
        }

        [JsonProperty("ch")]
        public string Channel { get; }

        [JsonProperty("ts")]
        public long Timestamp { get; }

        public TTick Tick { get; }
    }
}