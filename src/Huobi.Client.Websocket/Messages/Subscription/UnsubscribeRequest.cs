using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Subscription
{
    public class UnsubscribeRequest : RequestBase
    {
        public UnsubscribeRequest(string symbol, SubscriptionType subscriptionType, string step, string reqId)
            : base(reqId)
        {
            Topic = $"market.{symbol}.{subscriptionType.ToTopicId()}.{step}";
        }

        [JsonProperty("unsub")]
        public string Topic { get; }
    }
}