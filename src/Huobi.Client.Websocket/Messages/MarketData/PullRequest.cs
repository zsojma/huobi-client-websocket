using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData;

public class PullRequest : RequestBase
{
    public PullRequest(string reqId, string symbol, SubscriptionType subscriptionType, string? step = null)
        : base(reqId)
    {
        Validations.ValidateInput(symbol, nameof(symbol));
            
        if (!string.IsNullOrEmpty(step))
        {
            step = $".{step}";
        }

        Topic = $"{HuobiConstants.MARKET_PREFIX}.{symbol}.{subscriptionType.ToTopicId()}{step}";
    }

    [JsonProperty("req")]
    public string Topic { get; }
}