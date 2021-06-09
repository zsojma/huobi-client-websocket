using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public abstract class AccountRequestBase
    {
        protected AccountRequestBase(string action, string? channel = null)
        {
            Validations.ValidateInput(action, nameof(action));
            // Validations.ValidateInput(channel, nameof(channel));

            Action = action;
            if (string.IsNullOrEmpty(channel) == false) {
                Channel = channel;
            }

        }

        // [JsonProperty("op")]
        // public string Op { get; }

        [JsonProperty("op")]
        public string Action { get; }

        [JsonProperty("topic")]
        public string? Channel { get; }
    }
}