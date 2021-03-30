using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription
{
    public class UpdateMessage<TTick>
        where TTick : class
    {
        [JsonConstructor]
        public UpdateMessage(string topic, long timestamp, TTick tick)
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