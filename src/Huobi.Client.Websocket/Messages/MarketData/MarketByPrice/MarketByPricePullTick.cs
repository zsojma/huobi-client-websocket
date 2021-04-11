using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPricePullTick
    {
        [JsonConstructor]
        public MarketByPricePullTick(string seqNum, decimal[][] bids, decimal[][] asks)
        {
            Validations.ValidateInput(bids, nameof(bids));
            Validations.ValidateInput(asks, nameof(asks));

            SeqNum = seqNum;
            Bids = bids;
            Asks = asks;
        }

        public string SeqNum { get; }
        public decimal[][] Bids { get; }
        public decimal[][] Asks { get; }
    }
}