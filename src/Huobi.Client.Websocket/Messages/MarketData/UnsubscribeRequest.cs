using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class UnsubscribeRequest : RequestBase
    {
        public UnsubscribeRequest(string reqId, string symbol, SubscriptionType subscriptionType, string? step = null)
            : base(reqId)
        {
            Validations.ValidateInput(symbol, nameof(symbol));

            if (!string.IsNullOrEmpty(step))
            {
                step = $".{step}";
            }

            Topic = $"market.{symbol}.{subscriptionType.ToTopicId()}{step}";
        }

        [JsonProperty("unsub")]
        public string Topic { get; }
    }
}