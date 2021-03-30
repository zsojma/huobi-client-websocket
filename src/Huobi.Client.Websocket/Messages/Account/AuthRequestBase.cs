using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthRequestBase
    {
        public AuthRequestBase(string action, string topic)
        {
            Action = action;
            Topic = topic;
        }

        [JsonProperty("action")]
        public string Action { get; }

        [JsonProperty("ch")]
        public string Topic { get; }
    }
}