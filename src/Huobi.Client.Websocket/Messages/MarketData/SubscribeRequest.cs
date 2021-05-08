using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class SubscribeRequest : RequestBase
    {
        public SubscribeRequest(string reqId, string symbol, SubscriptionType subscriptionType, string? step = null)
            : base(reqId)
        {
            if (!string.IsNullOrEmpty(step))
            {
                step = $".{step}";
            }

            Topic = $"{HuobiConstants.MARKET_PREFIX}.{symbol}.{subscriptionType.ToTopicId()}{step}";
        }

        [JsonProperty("sub")]
        public string Topic { get; }
    }
}