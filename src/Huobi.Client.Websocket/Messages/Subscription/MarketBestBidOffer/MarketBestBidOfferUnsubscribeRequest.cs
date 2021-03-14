using Huobi.Client.Websocket.Messages.Values;

namespace Huobi.Client.Websocket.Messages.Subscription.MarketBestBidOffer
{
    public class MarketBestBidOfferUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketBestBidOfferUnsubscribeRequest(
            string symbol,
            string reqId)
            : base(symbol, SubscriptionType.MarketBestBidOffer, null, reqId)
        {
        }
    }
}