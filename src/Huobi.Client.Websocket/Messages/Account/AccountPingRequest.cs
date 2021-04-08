using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountPingRequest
    {
        public AccountPingRequest(long timestampMs)
            : this("ping", new AccountMessageData(timestampMs))
        {
        }
        
        [JsonConstructor]
        internal AccountPingRequest(string action, AccountMessageData data)
        {
            Validations.ValidateInput(action, nameof(action));
            Validations.ValidateInput(data, nameof(data));

            Action = action;
            Data = data;
        }

        [JsonProperty("action")]
        public string Action { get; }
        
        [JsonProperty("data")]
        public AccountMessageData Data { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out AccountPingRequest response)
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