using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;
using NodaTime;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountRequestParams
    {
        public AccountRequestParams(string accessKey, string signature, ZonedDateTime timestamp)
        {
            AuthType = "api";
            AccessKey = accessKey;
            SignatureMethod = HuobiSignature.SIGNATURE_METHOD_VALUE;
            SignatureVersion = HuobiSignature.SIGNATURE_VERSION_VERSION;
            Timestamp = timestamp.ToHuobiUtcString();
            Signature = signature;
        }

        [JsonProperty("authType")]
        public string AuthType { get; }

        [JsonProperty("accessKey")]
        public string AccessKey { get; }

        [JsonProperty("signatureMethod")]
        public string SignatureMethod { get; }

        [JsonProperty("signatureVersion")]
        public string SignatureVersion { get; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; }

        [JsonProperty("signature")]
        public string Signature { get; }
    }
}