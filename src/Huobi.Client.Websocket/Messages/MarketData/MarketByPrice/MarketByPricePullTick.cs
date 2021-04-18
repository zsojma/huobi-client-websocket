using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPricePullTick
    {
        [JsonConstructor]
        public MarketByPricePullTick(long seqNum, BookLevel[] bids, BookLevel[] asks)
        {
            Validations.ValidateInput(bids, nameof(bids));
            Validations.ValidateInput(asks, nameof(asks));

            SeqNum = seqNum;
            Bids = bids;
            Asks = asks;
        }

        public long SeqNum { get; }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Bid)]
        public BookLevel[] Bids { get; }

        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Ask)]
        public BookLevel[] Asks { get; }
    }
}