using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountMessageData
    {
        public AccountMessageData(long timestamp)
        {
            Timestamp = timestamp;
        }
        
        [JsonProperty("ts")]
        public long Timestamp { get; }
    }
}