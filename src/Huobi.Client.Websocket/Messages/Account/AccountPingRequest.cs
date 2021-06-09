using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountPingRequest
    {
        public AccountPingRequest(DateTimeOffset timestamp)
            : this("ping", new AccountMessageData(timestamp))
        {
        }

        [JsonConstructor]
        internal AccountPingRequest(string? action, AccountMessageData? data)
        {
            Action = action ?? string.Empty;
            Data = data;
        }

        [JsonProperty("op")]
        public string Action { get; }

        [JsonProperty("data")]
        public AccountMessageData? Data { get; }

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