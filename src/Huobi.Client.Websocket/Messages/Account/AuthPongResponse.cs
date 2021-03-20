using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthPongResponse
    {
        public AuthPongResponse(long timestamp)
        {
            Action = "pong";
            Data = new AuthMessageData(timestamp);
        }

        [JsonProperty("action")]
        public string Action { get; }
        
        [JsonProperty("data")]
        public AuthMessageData Data { get; }
    }
}