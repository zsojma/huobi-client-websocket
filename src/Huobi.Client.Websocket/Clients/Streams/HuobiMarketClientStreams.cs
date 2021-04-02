using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketDepth;
using Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketDetails;
using Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketTradeDetail;
using Huobi.Client.Websocket.Messages.MarketData.Subscription;
using Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketBestBidOffer;
using Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketDepth;
using Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketDetails;
using Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketTradeDetail;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiMarketClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<SubscribeResponse> SubscribeResponseSubject = new();
        internal readonly Subject<UnsubscribeResponse> UnsubscribeResponseSubject = new();

        internal readonly Subject<MarketCandlestickUpdateMessage> CandlestickUpdateSubject = new();
        internal readonly Subject<MarketDepthUpdateMessage> DepthUpdateSubject = new();
        internal readonly Subject<MarketByPriceUpdateMessage> MarketByPriceUpdateSubject = new();
        internal readonly Subject<MarketByPriceRefreshUpdateMessage> MarketByPriceRefreshUpdateSubject = new();
        internal readonly Subject<MarketBestBidOfferUpdateMessage> BestBidOfferUpdateSubject = new();
        internal readonly Subject<MarketTradeDetailUpdateMessage> TradeDetailUpdateSubject = new();
        internal readonly Subject<MarketDetailsUpdateMessage> MarketDetailsUpdateSubject = new();

        internal readonly Subject<MarketCandlestickPullResponse> CandlestickPullSubject = new();
        internal readonly Subject<MarketDepthPullResponse> DepthPullSubject = new();
        internal readonly Subject<MarketByPricePullResponse> MarketByPricePullSubject = new();
        internal readonly Subject<MarketTradeDetailPullResponse> TradeDetailPullSubject = new();
        internal readonly Subject<MarketDetailsPullResponse> MarketDetailsPullSubject = new();

        public IObservable<SubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
        public IObservable<UnsubscribeResponse> UnsubscribeResponseStream => UnsubscribeResponseSubject.AsObservable();

        public IObservable<MarketCandlestickUpdateMessage> CandlestickUpdateStream => CandlestickUpdateSubject.AsObservable();
        public IObservable<MarketDepthUpdateMessage> DepthUpdateStream => DepthUpdateSubject.AsObservable();
        public IObservable<MarketByPriceUpdateMessage> MarketByPriceUpdateStream => MarketByPriceUpdateSubject.AsObservable();
        public IObservable<MarketByPriceRefreshUpdateMessage> MarketByPriceRefreshUpdateStream => MarketByPriceRefreshUpdateSubject.AsObservable();
        public IObservable<MarketBestBidOfferUpdateMessage> BestBidOfferUpdateStream => BestBidOfferUpdateSubject.AsObservable();
        public IObservable<MarketTradeDetailUpdateMessage> TradeDetailUpdateStream => TradeDetailUpdateSubject.AsObservable();
        public IObservable<MarketDetailsUpdateMessage> MarketDetailsUpdateStream => MarketDetailsUpdateSubject.AsObservable();

        public IObservable<MarketCandlestickPullResponse> CandlestickPullStream => CandlestickPullSubject.AsObservable();
        public IObservable<MarketDepthPullResponse> DepthPullStream => DepthPullSubject.AsObservable();
        public IObservable<MarketByPricePullResponse> MarketByPricePullStream => MarketByPricePullSubject.AsObservable();
        public IObservable<MarketTradeDetailPullResponse> TradeDetailPullStream => TradeDetailPullSubject.AsObservable();
        public IObservable<MarketDetailsPullResponse> MarketDetailsPullStream => MarketDetailsPullSubject.AsObservable();
    }
}