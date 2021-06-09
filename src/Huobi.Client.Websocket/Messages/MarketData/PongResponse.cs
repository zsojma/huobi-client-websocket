using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class PongResponse
    {
        [JsonConstructor]
        public PongResponse(long value)
        {
            Value = value;
            Op = "pong";
        }

        [JsonProperty("ts")]
        public long Value { get; }

        [JsonProperty("op")]
        public string Op { get; }
    }
}