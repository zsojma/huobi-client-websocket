using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;

public class MarketBestBidOfferUpdateMessage : UpdateMessage<MarketBestBidOfferTick>
{
    public MarketBestBidOfferUpdateMessage(string topic, DateTimeOffset timestamp, MarketBestBidOfferTick tick)
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

        return result && !string.IsNullOrEmpty(response?.Tick?.Symbol);
    }
}