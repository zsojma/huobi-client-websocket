using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer.Converters;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPriceTick
    {
        [JsonConstructor]
        public MarketByPriceTick(long seqNum, long prevSeqNum, BookLevel[]? bids, BookLevel[]? asks)
        {
            SeqNum = seqNum;
            PrevSeqNum = prevSeqNum;
            Bids = bids;
            Asks = asks;
        }

        public long SeqNum { get; }
        public long PrevSeqNum { get; }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Bid)]
        public BookLevel[]? Bids { get; }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Ask)]
        public BookLevel[]? Asks { get; }
    }
}