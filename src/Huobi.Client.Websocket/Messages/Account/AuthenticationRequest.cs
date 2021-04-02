using Newtonsoft.Json;
using NodaTime;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthenticationRequest : AccountRequestBase
    {
        public AuthenticationRequest(string accessKey, string signature, ZonedDateTime timestamp)
            : base("req", "auth")
        {
            Parameters = new AccountRequestParams(accessKey, signature, timestamp);
        }

        [JsonProperty("params")]
        public AccountRequestParams Parameters { get; }
    }
}