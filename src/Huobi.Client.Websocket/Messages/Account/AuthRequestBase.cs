using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;
using NodaTime;

namespace Huobi.Client.Websocket.Messages.Account
{
    public abstract class AuthRequestBase
    {
        protected AuthRequestBase(string type, string symbol, AuthRequestParams parameters)
        {
            Action = "sub";
            Topic = $"{type}#{symbol}";
            Parameters = parameters;
        }
        
        [JsonProperty("action")]
        public string Action { get; }

        [JsonProperty("ch")]
        public string Topic { get; }

        [JsonProperty("params")]
        public AuthRequestParams Parameters { get; }
    }

    public class AuthRequestParams
    {
        public AuthRequestParams(string accessKey, ZonedDateTime timestamp)
        {
            AuthType = "api";
            AccessKey = accessKey;
            SignatureMethod = "HmacSHA256";
            SignatureVersion = "2.1";
            Timestamp = timestamp.ToDateTimeUtc().ToString("yyyy-MM-ddTHH:mm:ss");
            Signature = "4F65x5A2bLyMWVQj3Aqp+B4w+ivaA7n5Oi2SuYtCJ9o=";
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

    public class OrdersSubscribeRequest : AuthRequestBase
    {
        public OrdersSubscribeRequest(string symbol, string accessKey, ZonedDateTime timestamp)
            : base(AuthSubscriptionType.Orders.ToTopicId(), symbol, new AuthRequestParams(accessKey, timestamp))
        {
        }
    }
}