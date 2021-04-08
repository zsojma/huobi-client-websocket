using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer
{
    public class MarketBestBidOfferTick
    {
        [JsonConstructor]
        public MarketBestBidOfferTick(
            long quoteTimeMs,
            string symbol,
            decimal bid,
            decimal bidSize,
            decimal ask,
            decimal askSize,
            long seqId)
        {
            QuoteTimeMs = quoteTimeMs;

            Symbol = symbol;
            Bid = bid;
            BidSize = bidSize;
            Ask = ask;
            AskSize = askSize;
            SeqId = seqId;
        }

        [JsonIgnore]
        public DateTimeOffset QuoteTime => DateTimeOffset.FromUnixTimeMilliseconds(QuoteTimeMs);

        public string Symbol { get; }
        public decimal Bid { get; }
        public decimal BidSize { get; }
        public decimal Ask { get; }
        public decimal AskSize { get; }
        public long SeqId { get; }

        [JsonProperty("quoteTime")]
        internal long QuoteTimeMs { get; }
    }
}