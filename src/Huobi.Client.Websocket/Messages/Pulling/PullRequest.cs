using Huobi.Client.Websocket.Messages.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Pulling
{
    public abstract class PullRequest : RequestBase
    {
        protected PullRequest(string symbol, SubscriptionType subscriptionType, string step, string? reqId = null)
            : base(reqId)
        {
            Topic = $"market.{symbol}.{subscriptionType.ToTopicId()}.{step}";
        }

        [JsonProperty("req")]
        public string Topic { get; }
    }
}