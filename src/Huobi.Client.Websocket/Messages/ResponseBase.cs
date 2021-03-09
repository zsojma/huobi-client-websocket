using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public abstract class ResponseBase
    {
        [JsonConstructor]
        public ResponseBase(string reqId)
        {
            ReqId = reqId;
        }

        [JsonProperty("id")]
        public string ReqId { get; }
    }
}