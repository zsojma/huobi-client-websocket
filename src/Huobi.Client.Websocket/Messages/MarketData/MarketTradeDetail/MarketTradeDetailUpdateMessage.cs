﻿using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;

public class MarketTradeDetailUpdateMessage : UpdateMessage<MarketTradeDetailTick>
{
    public MarketTradeDetailUpdateMessage(string topic, DateTimeOffset timestamp, MarketTradeDetailTick tick)
        : base(topic, timestamp, tick)
    {
    }

    internal static bool TryParse(
        IHuobiSerializer serializer,
        string input,
        [MaybeNullWhen(false)] out MarketTradeDetailUpdateMessage response)
    {
        var result = serializer.TryDeserializeIfContains(
            input,
            new[]
            {
                "\"tick\"",
                SubscriptionType.MarketTradeDetail.ToTopicId()
            },
            out response);

        return result && response?.Tick?.Timestamp.Ticks > 0;
    }
}