using Huobi.Client.Websocket.Messages.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Subscription
{
    public abstract class SubscribeRequest : RequestBase
    {
        protected SubscribeRequest(string symbol, SubscriptionType subscriptionType, string? step, string? reqId = null)
            : base(reqId)
        {
            if (!string.IsNullOrEmpty(step))
            {
                step = $".{step}";
            }

            Topic = $"market.{symbol}.{subscriptionType.ToTopicId()}{step}";
        }

        [JsonProperty("sub")]
        public string Topic { get; }
    }
}