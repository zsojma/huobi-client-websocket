using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public abstract class FuturesRequestBase
    {
        protected FuturesRequestBase(string op)
        {
            Validations.ValidateInput(op, nameof(op));

            Op = op;
            // Op = "auth";
        }

        [JsonProperty("op")]
        public string Op { get; }
    }
}