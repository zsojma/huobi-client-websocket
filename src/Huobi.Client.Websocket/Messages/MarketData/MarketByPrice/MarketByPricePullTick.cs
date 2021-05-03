using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer.Converters;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPricePullTick
    {
        [JsonConstructor]
        public MarketByPricePullTick(long seqNum, BookLevel[]? bids, BookLevel[]? asks)
        {
            SeqNum = seqNum;
            Bids = bids ?? Array.Empty<BookLevel>();
            Asks = asks ?? Array.Empty<BookLevel>();
        }

        public long SeqNum { get; }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Bid)]
        public BookLevel[] Bids { get; }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Ask)]
        public BookLevel[] Asks { get; }
    }
}