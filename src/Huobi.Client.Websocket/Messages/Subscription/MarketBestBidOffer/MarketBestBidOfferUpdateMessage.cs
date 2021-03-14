﻿using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Ticks;
using Huobi.Client.Websocket.Messages.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketBestBidOffer
{
    public class MarketBestBidOfferUpdateMessage : UpdateMessage<MarketBestBidOfferTick>
    {
        public MarketBestBidOfferUpdateMessage(string topic, long timestamp, MarketBestBidOfferTick tick)
            : base(topic, timestamp, tick)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketBestBidOfferUpdateMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"tick\"",
                    SubscriptionType.MarketBestBidOffer.ToTopicId()
                },
                out response);

            return result && !string.IsNullOrEmpty(response?.Tick.Symbol);
        }
    }
}