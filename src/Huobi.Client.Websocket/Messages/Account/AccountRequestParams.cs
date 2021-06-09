using System;
using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountRequestParams
    {
        public AccountRequestParams(string accessKey, string signature, DateTimeOffset timestamp)
        {
            Validations.ValidateInput(accessKey, nameof(accessKey));
            Validations.ValidateInput(signature, nameof(signature));

            type = "api";
            AccessKey = accessKey;
            SignatureMethod = HuobiSignature.SIGNATURE_METHOD_VALUE;
            SignatureVersion = HuobiSignature.SIGNATURE_VERSION_VERSION;
            Timestamp = timestamp.ToHuobiUtcString();
            Signature = signature;
            Op = "auth";
        }

        [JsonProperty("type")]
        public string type { get; }

        [JsonProperty("op")]
        public string Op { get; }

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