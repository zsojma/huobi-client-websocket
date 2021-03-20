﻿using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Pulling
{
    public abstract class PullRequest : RequestBase
    {
        protected PullRequest(string symbol, SubscriptionType subscriptionType, string? step, string? reqId = null)
            : base(reqId)
        {
            if (!string.IsNullOrEmpty(step))
            {
                step = $".{step}";
            }

            Topic = $"market.{symbol}.{subscriptionType.ToTopicId()}{step}";
        }

        [JsonProperty("req")]
        public string Topic { get; }
    }
}