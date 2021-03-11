using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages;
using Huobi.Client.Websocket.Messages.Pulling.Candlestick;
using Huobi.Client.Websocket.Messages.Pulling.Depth;
using Huobi.Client.Websocket.Messages.Subscription;
using Huobi.Client.Websocket.Messages.Subscription.Candlestick;
using Huobi.Client.Websocket.Messages.Subscription.Depth;

namespace Huobi.Client.Websocket.Client
{
    public class HuobiClientStreams
    {
        internal readonly Subject<string> UnhandledMessageSubject = new Subject<string>();
        internal readonly Subject<ErrorMessage> ErrorMessageSubject = new Subject<ErrorMessage>();
        internal readonly Subject<SubscribeResponse> SubscribeResponseSubject = new Subject<SubscribeResponse>();
        internal readonly Subject<UnsubscribeResponse> UnsubscribeResponseSubject = new Subject<UnsubscribeResponse>();

        internal readonly Subject<MarketCandlestickUpdateMessage> MarketCandlestickUpdateSubject = new Subject<MarketCandlestickUpdateMessage>();
        internal readonly Subject<MarketDepthUpdateMessage> MarketDepthUpdateSubject = new Subject<MarketDepthUpdateMessage>();

        internal readonly Subject<MarketCandlestickPullResponse> MarketCandlestickPullSubject = new Subject<MarketCandlestickPullResponse>();
        internal readonly Subject<MarketDepthPullResponse> MarketDepthPullSubject = new Subject<MarketDepthPullResponse>();

        public IObservable<string> UnhandledMessageStream => UnhandledMessageSubject.AsObservable();
        public IObservable<ErrorMessage> ErrorMessageStream => ErrorMessageSubject.AsObservable();
        public IObservable<SubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
        public IObservable<UnsubscribeResponse> UnsubscribeResponseStream => UnsubscribeResponseSubject.AsObservable();
        public IObservable<MarketCandlestickUpdateMessage> MarketCandlestickUpdateStream => MarketCandlestickUpdateSubject.AsObservable();
        public IObservable<MarketDepthUpdateMessage> MarketDepthUpdateStream => MarketDepthUpdateSubject.AsObservable();
        public IObservable<MarketCandlestickPullResponse> MarketCandlestickPullStream => MarketCandlestickPullSubject.AsObservable();
        public IObservable<MarketDepthPullResponse> MarketDepthPullStream => MarketDepthPullSubject.AsObservable();
    }
}