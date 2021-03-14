﻿using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Ticks;
using Huobi.Client.Websocket.Messages.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketByPrice
{
    public class MarketByPriceUpdateMessage : UpdateMessage<MarketByPriceTick>
    {
        public MarketByPriceUpdateMessage(string topic, long timestamp, MarketByPriceTick tick)
            : base(topic, timestamp, tick)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketByPriceUpdateMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"tick\"",
                    SubscriptionType.MarketByPrice.ToTopicId()
                },
                new[]
                {
                    SubscriptionType.MarketByPriceRefresh.ToTopicId()
                },
                out response);

            return result && !string.IsNullOrEmpty(response?.Tick.SeqNum);
        }
    }
}