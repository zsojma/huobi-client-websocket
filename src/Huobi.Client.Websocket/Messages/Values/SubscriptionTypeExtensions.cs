﻿using System;

namespace Huobi.Client.Websocket.Messages.Values
{
    public static class SubscriptionTypeExtensions
    {
        public static string ToTopicId(this SubscriptionType subscriptionType)
        {
            return subscriptionType switch
            {
                SubscriptionType.MarketCandlestick => "kline",
                SubscriptionType.MarketDepth => "depth",
                SubscriptionType.MarketByPrice => "mbp",
                SubscriptionType.MarketByPriceRefresh => "mbp.refresh",
                SubscriptionType.MarketBestBidOffer => "bbo",
                SubscriptionType.MarketTradeDetail => "trade.detail",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(subscriptionType),
                    subscriptionType,
                    $"Unable to translate {nameof(subscriptionType)} value '{subscriptionType}' to topic ID!")
            };
        }
    }
}