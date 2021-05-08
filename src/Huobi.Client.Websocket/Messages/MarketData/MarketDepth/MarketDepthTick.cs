using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer.Converters;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDepth
{
    public class MarketDepthTick
    {
        [JsonConstructor]
        public MarketDepthTick(BookLevel[]? bids, BookLevel[]? asks, long version, DateTimeOffset timestamp)
        {
            Bids = bids ?? Array.Empty<BookLevel>();
            Asks = asks ?? Array.Empty<BookLevel>();
            Version = version;
            Timestamp = timestamp;
        }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Bid)]
        public BookLevel[] Bids { get; }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Ask)]
        public BookLevel[] Asks { get; }

        public long Version { get; }

        [JsonProperty("ts")]
        public DateTimeOffset Timestamp { get; }
    }
}