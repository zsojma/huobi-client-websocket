using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class FuturesAuthenticationRequest : FuturesRequestBase
    {
        public FuturesAuthenticationRequest(string accessKey, string signature, DateTimeOffset timestamp)
            : base("auth")
        {
            Parameters = new AccountRequestParams(accessKey, signature, timestamp);
        }

        [JsonProperty("params")]
        public AccountRequestParams Parameters { get; }
    }
}