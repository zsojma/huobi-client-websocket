using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData;

public class PongResponse
{
    [JsonConstructor]
    public PongResponse(long value)
    {
        Value = value;
    }

    [JsonProperty("pong")]
    public long Value { get; }
}