using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer
{
    public class MarketBestBidOfferSubscribeRequest : SubscribeRequest
    {
        public MarketBestBidOfferSubscribeRequest(
            string symbol,
            string reqId)
            : base(symbol, SubscriptionType.MarketBestBidOffer, null, reqId)
        {
        }
    }
}