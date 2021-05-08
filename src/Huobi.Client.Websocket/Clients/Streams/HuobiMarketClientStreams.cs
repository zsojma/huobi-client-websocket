using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.MarketDepth;
using Huobi.Client.Websocket.Messages.MarketData.MarketDetails;
using Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiMarketClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<SubscribeResponse> SubscribeResponseSubject = new();
        internal readonly Subject<UnsubscribeResponse> UnsubscribeResponseSubject = new();

        internal readonly Subject<MarketCandlestickUpdateMessage> CandlestickUpdateSubject = new();
        internal readonly Subject<MarketDepthUpdateMessage> DepthUpdateSubject = new();
        internal readonly Subject<MarketByPriceRefreshUpdateMessage> MarketByPriceRefreshUpdateSubject = new();
        internal readonly Subject<MarketBestBidOfferUpdateMessage> BestBidOfferUpdateSubject = new();
        internal readonly Subject<MarketTradeDetailUpdateMessage> TradeDetailUpdateSubject = new();
        internal readonly Subject<MarketDetailsUpdateMessage> MarketDetailsUpdateSubject = new();

        internal readonly Subject<MarketCandlestickPullResponse> CandlestickPullSubject = new();
        internal readonly Subject<MarketDepthPullResponse> DepthPullSubject = new();
        internal readonly Subject<MarketTradeDetailPullResponse> TradeDetailPullSubject = new();
        internal readonly Subject<MarketDetailsPullResponse> MarketDetailsPullSubject = new();

        public IObservable<SubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
        public IObservable<UnsubscribeResponse> UnsubscribeResponseStream => UnsubscribeResponseSubject.AsObservable();

        public IObservable<MarketCandlestickUpdateMessage> CandlestickUpdateStream => CandlestickUpdateSubject.AsObservable();
        public IObservable<MarketDepthUpdateMessage> DepthUpdateStream => DepthUpdateSubject.AsObservable();
        public IObservable<MarketBestBidOfferUpdateMessage> BestBidOfferUpdateStream => BestBidOfferUpdateSubject.AsObservable();
        public IObservable<MarketTradeDetailUpdateMessage> TradeDetailUpdateStream => TradeDetailUpdateSubject.AsObservable();
        public IObservable<MarketDetailsUpdateMessage> MarketDetailsUpdateStream => MarketDetailsUpdateSubject.AsObservable();

        public IObservable<MarketCandlestickPullResponse> CandlestickPullStream => CandlestickPullSubject.AsObservable();
        public IObservable<MarketDepthPullResponse> DepthPullStream => DepthPullSubject.AsObservable();
        public IObservable<MarketByPriceRefreshUpdateMessage> MarketByPriceRefreshUpdateStream => MarketByPriceRefreshUpdateSubject.AsObservable();
        public IObservable<MarketTradeDetailPullResponse> TradeDetailPullStream => TradeDetailPullSubject.AsObservable();
        public IObservable<MarketDetailsPullResponse> MarketDetailsPullStream => MarketDetailsPullSubject.AsObservable();
    }
}