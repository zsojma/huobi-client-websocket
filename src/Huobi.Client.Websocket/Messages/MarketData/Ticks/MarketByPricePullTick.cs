using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.Ticks
{
    public class MarketByPricePullTick
    {
        [JsonConstructor]
        public MarketByPricePullTick(string seqNum, decimal[][] bids, decimal[][] asks)
        {
            SeqNum = seqNum;
            Bids = bids;
            Asks = asks;
        }

        public string SeqNum { get; }
        public decimal[][] Bids { get; }
        public decimal[][] Asks { get; }
    }
}