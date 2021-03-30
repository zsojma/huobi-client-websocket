using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Pulling
{
    public class PullResponse<TTick> : ResponseBase
        where TTick : class
    {
        public PullResponse(string reqId, string status, string topic, long timestamp, TTick data)
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