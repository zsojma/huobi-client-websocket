using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public abstract class AccountResponseBase<TData>
        where TData : class
    {
        protected AccountResponseBase(string? action, string? topic, TData? data)
        {
            Action = action ?? string.Empty;
            Topic = topic ?? string.Empty;
            Data = data;
        }

        [JsonProperty("op")]
        public string Action { get; }

        [JsonProperty("topic")]
        public string Topic { get; }

        public TData? Data { get; }
    }
}