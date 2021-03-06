using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public class PingMessage
    {
        [JsonConstructor]
        public PingMessage(long value)
        {
            Value = value;
        }

        [JsonProperty("ping")]
        public long Value { get; }
    }
}