using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class PingRequest
    {
        [JsonConstructor]
        public PingRequest(long value)
        {
            Value = value;
        }

        [JsonProperty("ping")]
        public long Value { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out PingRequest response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"ping\""
                },
                out response);

            return result && response?.Value > 0;
        }
    }
}