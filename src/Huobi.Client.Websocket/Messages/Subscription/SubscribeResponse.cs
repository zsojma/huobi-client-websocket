using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Subscription
{
    public class SubscribeResponse : ResponseBase
    {
        [JsonConstructor]
        public SubscribeResponse(string reqId, string status, string subbed, long timestamp)
            : base(reqId)
        {
            Status = status;
            Subbed = subbed;
            Timestamp = timestamp;
        }

        [JsonProperty("status")]
        public string Status { get; }

        [JsonProperty("subbed")]
        public string Subbed { get; }

        [JsonProperty("ts")]
        public long Timestamp { get; }

        internal static bool TryParse(IHuobiSerializer serializer, string input, [MaybeNullWhen(false)] out SubscribeResponse response)
        {
            return serializer.TryDeserializeIfContains(input, "\"subbed\"", out response);
        }
    }
}