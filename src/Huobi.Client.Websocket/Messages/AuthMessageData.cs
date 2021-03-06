using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public class AuthMessageData
    {
        public AuthMessageData(long ts)
        {
            Ts = ts;
        }
        
        [JsonProperty("ts")]
        public long Ts { get; }
    }
}