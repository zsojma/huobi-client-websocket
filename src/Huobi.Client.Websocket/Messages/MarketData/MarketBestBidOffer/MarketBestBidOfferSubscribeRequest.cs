using Huobi.Client.Websocket.Messages.MarketData.Values;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;

public class MarketBestBidOfferSubscribeRequest : SubscribeRequest
{
    public MarketBestBidOfferSubscribeRequest(
        string reqId,
        string symbol)
        : base(reqId, symbol, SubscriptionType.MarketBestBidOffer)
    {
    }
}