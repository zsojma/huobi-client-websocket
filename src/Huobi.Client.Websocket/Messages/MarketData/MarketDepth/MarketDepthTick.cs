using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDepth
{
    public class MarketDepthTick
    {
        [JsonConstructor]
        public MarketDepthTick(BookLevel[] bids, BookLevel[] asks, long version, DateTimeOffset timestamp)
        {
            Validations.ValidateInput(bids, nameof(bids));
            Validations.ValidateInput(asks, nameof(asks));

            Bids = bids;
            Asks = asks;
            Version = version;
            Timestamp = timestamp;
        }
        
        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Bid)]
        public BookLevel[] Bids { get; }
        
        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Ask)]
        public BookLevel[] Asks { get; }

        public long Version { get; }

        [JsonProperty("ts")]
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; }
    }
}