using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountPongResponse
    {
        public AccountPongResponse(long timestampMs)
        {
            Action = "pong";
            Data = new AccountMessageData(timestampMs);
        }

        [JsonProperty("action")]
        public string Action { get; }
        
        [JsonProperty("data")]
        public AccountMessageData Data { get; }
    }
}