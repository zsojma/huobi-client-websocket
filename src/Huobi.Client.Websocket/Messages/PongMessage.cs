using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public class PongMessage
    {
        [JsonConstructor]
        public PongMessage(long value)
        {
            Value = value;
        }

        [JsonProperty("pong")]
        public long Value { get; }
    }
}