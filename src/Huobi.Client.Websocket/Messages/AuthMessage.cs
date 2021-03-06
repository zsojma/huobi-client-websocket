using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages
{
    public class AuthMessage
    {
        public AuthMessage(string action, AuthMessageData data)
        {
            Action = action;
            Data = data;
        }

        [JsonProperty("action")]
        public string Action { get; }
        
        [JsonProperty("data")]
        public AuthMessageData Data { get; }
    }
}