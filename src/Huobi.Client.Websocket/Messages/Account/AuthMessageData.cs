using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthMessageData
    {
        public AuthMessageData(long timestamp)
        {
            Timestamp = timestamp;
        }
        
        [JsonProperty("ts")]
        public long Timestamp { get; }
    }
}