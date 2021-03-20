using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Subscription
{
    public abstract class UnsubscribeRequest : RequestBase
    {
        protected UnsubscribeRequest(string symbol, SubscriptionType subscriptionType, string? step, string? reqId = null)
            : base(reqId)
        {
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