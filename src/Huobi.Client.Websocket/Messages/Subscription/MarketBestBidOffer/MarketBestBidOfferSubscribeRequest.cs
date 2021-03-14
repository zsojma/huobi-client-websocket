using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketBestBidOffer
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