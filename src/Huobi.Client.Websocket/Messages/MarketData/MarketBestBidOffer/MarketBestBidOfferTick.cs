using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;

public class MarketBestBidOfferTick
{
    [JsonConstructor]
    public MarketBestBidOfferTick(
        string symbol,
        DateTimeOffset quoteTime,
        decimal bid,
        decimal bidSize,
        decimal ask,
        decimal askSize,
        long seqId)
    {
        Symbol = symbol;
        QuoteTime = quoteTime;
        Bid = bid;
        BidSize = bidSize;
        Ask = ask;
        AskSize = askSize;
        SeqId = seqId;
    }
    public string Symbol { get; }
    public DateTimeOffset QuoteTime { get; }
    public decimal Bid { get; }
    public decimal BidSize { get; }
    public decimal Ask { get; }
    public decimal AskSize { get; }
    public long SeqId { get; }
}