using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public abstract class AccountResponseBase
    {
        protected AccountResponseBase(string action, int code, string channel, object data)
        {
            Action = action;
            Code = code;
            Channel = channel;
            Data = data;
        }


        public string Action { get; }
        public int Code { get; }

        [JsonProperty("ch")]
        public string Channel { get; }

        public object Data { get; }
    }
}