using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Pulling
{
    public abstract class PullResponse<TTick> : ResponseBase
        where TTick : class
    {
        protected PullResponse(string reqId, string status, string topic, long timestamp, TTick data)
            : base(reqId)
        {
            Status = status;
            Topic = topic;
            Timestamp = timestamp;
            Data = data;
        }

        public string Status { get; }

        [JsonProperty("rep")]
        public string Topic { get; }

        [JsonProperty("ts")]
        public long Timestamp { get; }

        public TTick Data { get; }
    }
}