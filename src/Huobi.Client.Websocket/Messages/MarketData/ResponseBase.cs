using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public abstract class ResponseBase
    {
        protected ResponseBase(string reqId)
        {
            Validations.ValidateInput(reqId, nameof(reqId));
            
            ReqId = reqId;
        }

        [JsonProperty("id")]
        public string ReqId { get; }
    }
}