using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.Subscription
{
    public class AccountSubscribeResponse : AccountResponseBase
    {
        [JsonConstructor]
        public AccountSubscribeResponse(string action, int code, string channel, object data)
            : base(action, code, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out AccountSubscribeResponse response)
        {
            return serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"sub\"",
                    "\"code\""
                },
                out response);
        }
    }
}