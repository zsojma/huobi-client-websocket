using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public abstract class ResponseBase
    {
        protected ResponseBase(string reqId)
        {
            ReqId = reqId;
        }

        [JsonProperty("id")]
        public string ReqId { get; }
    }
}