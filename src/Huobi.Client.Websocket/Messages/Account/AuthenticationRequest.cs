using System;
using Newtonsoft.Json;
using Huobi.Client.Websocket.Utils;
using Huobi.Client.Websocket.Authentication;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthenticationRequest : AccountRequestBase
    {

        public AuthenticationRequest(string accessKey, string signature, DateTimeOffset timestamp)
            : base("auth")
        {
            Validations.ValidateInput(accessKey, nameof(accessKey));
            Validations.ValidateInput(signature, nameof(signature));

            type = "api";
            AccessKey = accessKey;
            SignatureMethod = HuobiSignature.SIGNATURE_METHOD_VALUE;
            SignatureVersion = HuobiSignature.SIGNATURE_VERSION_VERSION;
            Timestamp = timestamp.ToHuobiUtcString();
            Signature = signature;
        }

        [JsonProperty("type")]
        public string type { get; }

        // [JsonProperty("op")]
        // public string Op { get; }

        [JsonProperty("AccessKeyId")]
        public string AccessKey { get; }

        [JsonProperty("SignatureMethod")]
        public string SignatureMethod { get; }

        [JsonProperty("SignatureVersion")]
        public string SignatureVersion { get; }

        [JsonProperty("Timestamp")]
        public string Timestamp { get; }

        [JsonProperty("Signature")]
        public string Signature { get; }
    }
}