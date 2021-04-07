using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public abstract class AccountResponseBase<TData>
        where TData : class
    {
        protected AccountResponseBase(string action, string channel, TData data)
        {
            Action = action;
            Channel = channel;
            Data = data;
        }


        public string Action { get; }

        [JsonProperty("ch")]
        public string Channel { get; }

        public TData Data { get; }
    }
}