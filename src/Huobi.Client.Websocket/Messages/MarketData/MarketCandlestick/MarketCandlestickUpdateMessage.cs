using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;

public class MarketCandlestickUpdateMessage : UpdateMessage<MarketCandlestickTick>
{
    public MarketCandlestickUpdateMessage(string topic, DateTimeOffset timestamp, MarketCandlestickTick tick)
        : base(topic, timestamp, tick)
    {
    }

    internal static bool TryParse(
        IHuobiSerializer serializer,
        string input,
        [MaybeNullWhen(false)] out MarketCandlestickUpdateMessage response)
    {
        var result = serializer.TryDeserializeIfContains(
            input,
            new[]
            {
                "\"tick\"",
                SubscriptionType.MarketCandlestick.ToTopicId()
            },
            out response);

        return result && response?.Tick?.Id > 0;
    }
}