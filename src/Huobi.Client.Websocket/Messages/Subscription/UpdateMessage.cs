using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Subscription
{
    public abstract class UpdateMessage<TTick>
        where TTick : class
    {
        [JsonConstructor]
        protected UpdateMessage(string topic, long timestamp, TTick tick)
        {
            Topic = topic;
            Timestamp = timestamp;
            Tick = tick;
        }

        [JsonProperty("ch")]
        public string Topic { get; }

        [JsonProperty("ts")]
        public long Timestamp { get; }

        public TTick Tick { get; }
    }
}