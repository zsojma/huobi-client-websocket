using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Ticks
{
    public class MarketByPriceTick
    {
        [JsonConstructor]
        public MarketByPriceTick(string seqNum, string prevSeqNum, decimal[][]? bids, decimal[][]? asks)
        {
            SeqNum = seqNum;
            PrevSeqNum = prevSeqNum;
            Bids = bids;
            Asks = asks;
        }

        public string SeqNum { get; }
        public string PrevSeqNum { get; }
        public decimal[][]? Bids { get; }
        public decimal[][]? Asks { get; }
    }
}