using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountRequestBase
    {
        public AccountRequestBase(string action, string channel)
        {
            Action = action;
            Channel = channel;
        }

        [JsonProperty("action")]
        public string Action { get; }

        [JsonProperty("ch")]
        public string Channel { get; }
    }
}