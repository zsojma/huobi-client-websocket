﻿using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class SubscribeRequest : RequestBase
    {
        public SubscribeRequest(string symbol, SubscriptionType subscriptionType, string? step, string? reqId = null)
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