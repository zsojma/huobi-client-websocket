using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Subscription
{
    public class SubscribeRequest : RequestBase
    {
        public SubscribeRequest(string symbol, SubscriptionType subscriptionType, string step, string reqId)
            : base(reqId)
        {
            Topic = $"market.{symbol}.{subscriptionType.ToTopicId()}.{step}";
        }

        [JsonProperty("sub")]
        public string Topic { get; }
    }
}