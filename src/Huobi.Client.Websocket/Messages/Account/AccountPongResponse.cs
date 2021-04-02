using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountPongResponse
    {
        public AccountPongResponse(long timestamp)
        {
            Action = "pong";
            Data = new AccountMessageData(timestamp);
        }

        [JsonProperty("action")]
        public string Action { get; }
        
        [JsonProperty("data")]
        public AccountMessageData Data { get; }
    }
}