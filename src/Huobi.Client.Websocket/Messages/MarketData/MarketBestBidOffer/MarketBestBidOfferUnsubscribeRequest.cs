using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer
{
    public class MarketBestBidOfferUnsubscribeRequest : UnsubscribeRequest
    {
        public MarketBestBidOfferUnsubscribeRequest(
            string reqId,
            string symbol)
            : base(reqId, symbol, SubscriptionType.MarketBestBidOffer)
        {
        }
    }
}