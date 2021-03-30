using Newtonsoft.Json;
using NodaTime;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthenticationRequest : AuthRequestBase
    {
        public AuthenticationRequest(string accessKey, string signature, ZonedDateTime timestamp)
            : base("req", "auth")
        {
            Parameters = new AuthRequestParams(accessKey, signature, timestamp);
        }

        [JsonProperty("params")]
        public AuthRequestParams Parameters { get; }
    }
}