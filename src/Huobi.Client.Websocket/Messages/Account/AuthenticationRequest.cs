using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthenticationRequest : AccountRequestBase
    {
        public AuthenticationRequest(string accessKey, string signature, DateTimeOffset timestamp)
            : base("req", "auth")
        {
            Parameters = new AccountRequestParams(accessKey, signature, timestamp);
        }

        [JsonProperty("params")]
        public AccountRequestParams Parameters { get; }
    }
}