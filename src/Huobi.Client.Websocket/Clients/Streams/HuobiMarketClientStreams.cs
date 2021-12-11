using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.MarketDepth;
using Huobi.Client.Websocket.Messages.MarketData.MarketDetails;
using Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;

namespace Huobi.Client.Websocket.Clients.Streams;

public class HuobiMarketClientStreams : HuobiClientStreamsBase
{
    public readonly Subject<SubscribeResponse> SubscribeResponseStream = new();
    public readonly Subject<UnsubscribeResponse> UnsubscribeResponseStream = new();

    public readonly Subject<MarketCandlestickUpdateMessage> CandlestickUpdateStream = new();
    public readonly Subject<MarketDepthUpdateMessage> DepthUpdateStream = new();
    public readonly Subject<MarketBestBidOfferUpdateMessage> BestBidOfferUpdateStream = new();
    public readonly Subject<MarketTradeDetailUpdateMessage> TradeDetailUpdateStream = new();
    public readonly Subject<MarketDetailsUpdateMessage> MarketDetailsUpdateStream = new();

    public readonly Subject<MarketCandlestickPullResponse> CandlestickPullStream = new();
    public readonly Subject<MarketDepthPullResponse> DepthPullStream = new();
    public readonly Subject<MarketByPriceRefreshUpdateMessage> MarketByPriceRefreshUpdateStream = new();
    public readonly Subject<MarketTradeDetailPullResponse> TradeDetailPullStream = new();
    public readonly Subject<MarketDetailsPullResponse> MarketDetailsPullStream = new();
}