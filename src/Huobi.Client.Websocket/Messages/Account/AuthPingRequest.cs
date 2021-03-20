using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthPingRequest
    {
        public AuthPingRequest(long timestamp)
            : this("ping", new AuthMessageData(timestamp))
        {
        }
        
        [JsonConstructor]
        internal AuthPingRequest(string action, AuthMessageData data)
        {
            Action = action;
            Data = data;
        }

        [JsonProperty("action")]
        public string Action { get; }
        
        [JsonProperty("data")]
        public AuthMessageData Data { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out AuthPingRequest response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"ping\""
                },
                out response);

            return result && string.Equals(response?.Action, "ping");
        }
    }
}