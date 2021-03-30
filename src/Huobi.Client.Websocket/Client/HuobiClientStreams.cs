using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.MarketData;
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

namespace Huobi.Client.Websocket.Client
{
    public class HuobiClientStreams
    {
        internal readonly Subject<string> UnhandledMessageSubject = new();
        internal readonly Subject<ErrorMessage> ErrorMessageSubject = new();
        internal readonly Subject<PingRequest> PingMessageSubject = new();
        internal readonly Subject<AuthPingRequest> PingAuthMessageSubject = new();
        internal readonly Subject<SubscribeResponse> SubscribeResponseSubject = new();
        internal readonly Subject<UnsubscribeResponse> UnsubscribeResponseSubject = new();

        internal readonly Subject<MarketCandlestickUpdateMessage> MarketCandlestickUpdateSubject = new();
        internal readonly Subject<MarketDepthUpdateMessage> MarketDepthUpdateSubject = new();
        internal readonly Subject<MarketByPriceUpdateMessage> MarketByPriceUpdateSubject = new();
        internal readonly Subject<MarketByPriceRefreshUpdateMessage> MarketByPriceRefreshUpdateSubject = new();
        internal readonly Subject<MarketBestBidOfferUpdateMessage> MarketBestBidOfferUpdateSubject = new();
        internal readonly Subject<MarketTradeDetailUpdateMessage> MarketTradeDetailUpdateSubject = new();
        internal readonly Subject<MarketDetailsUpdateMessage> MarketDetailsUpdateSubject = new();

        internal readonly Subject<MarketCandlestickPullResponse> MarketCandlestickPullSubject = new();
        internal readonly Subject<MarketDepthPullResponse> MarketDepthPullSubject = new();
        internal readonly Subject<MarketByPricePullResponse> MarketByPricePullSubject = new();
        internal readonly Subject<MarketTradeDetailPullResponse> MarketTradeDetailPullSubject = new();
        internal readonly Subject<MarketDetailsPullResponse> MarketDetailsPullSubject = new();

        internal readonly Subject<AuthErrorMessage> AuthErrorMessageSubject = new();
        internal readonly Subject<AuthenticationResponse> AuthenticationResponseSubject = new();

        public IObservable<string> UnhandledMessageStream => UnhandledMessageSubject.AsObservable();
        public IObservable<ErrorMessage> ErrorMessageStream => ErrorMessageSubject.AsObservable();
        public IObservable<PingRequest> PingMessageStream => PingMessageSubject.AsObservable();
        public IObservable<AuthPingRequest> PingAuthMessageStream => PingAuthMessageSubject.AsObservable();
        public IObservable<SubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
        public IObservable<UnsubscribeResponse> UnsubscribeResponseStream => UnsubscribeResponseSubject.AsObservable();
        
        public IObservable<MarketCandlestickUpdateMessage> MarketCandlestickUpdateStream => MarketCandlestickUpdateSubject.AsObservable();
        public IObservable<MarketDepthUpdateMessage> MarketDepthUpdateStream => MarketDepthUpdateSubject.AsObservable();
        public IObservable<MarketByPriceUpdateMessage> MarketByPriceUpdateStream => MarketByPriceUpdateSubject.AsObservable();
        public IObservable<MarketByPriceRefreshUpdateMessage> MarketByPriceRefreshUpdateStream => MarketByPriceRefreshUpdateSubject.AsObservable();
        public IObservable<MarketBestBidOfferUpdateMessage> MarketBestBidOfferUpdateStream => MarketBestBidOfferUpdateSubject.AsObservable();
        public IObservable<MarketTradeDetailUpdateMessage> MarketTradeDetailUpdateStream => MarketTradeDetailUpdateSubject.AsObservable();
        public IObservable<MarketDetailsUpdateMessage> MarketDetailsUpdateStream => MarketDetailsUpdateSubject.AsObservable();
        
        public IObservable<MarketCandlestickPullResponse> MarketCandlestickPullStream => MarketCandlestickPullSubject.AsObservable();
        public IObservable<MarketDepthPullResponse> MarketDepthPullStream => MarketDepthPullSubject.AsObservable();
        public IObservable<MarketByPricePullResponse> MarketByPricePullStream => MarketByPricePullSubject.AsObservable();
        public IObservable<MarketTradeDetailPullResponse> MarketTradeDetailPullStream => MarketTradeDetailPullSubject.AsObservable();
        public IObservable<MarketDetailsPullResponse> MarketDetailsPullStream => MarketDetailsPullSubject.AsObservable();
        
        public IObservable<AuthErrorMessage> AuthErrorMessageStream => AuthErrorMessageSubject.AsObservable();
        public IObservable<AuthenticationResponse> AuthenticationResponseStream => AuthenticationResponseSubject.AsObservable();
    }
}