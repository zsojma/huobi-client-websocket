using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public abstract class RequestBase
    {
        public RequestBase(string reqId)
        {
            ReqId = reqId;
        }

        [JsonProperty("id")]
        public string ReqId { get; }
    }
}