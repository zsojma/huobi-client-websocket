using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages;
using Huobi.Client.Websocket.Messages.Subscription;
using Huobi.Client.Websocket.Messages.Subscription.Ticks;

namespace Huobi.Client.Websocket.Client
{
    public class HuobiClientStreams
    {
        internal readonly Subject<string> UnhandledMessageSubject = new Subject<string>();
        internal readonly Subject<SubscribeResponse> SubscribeResponseSubject = new Subject<SubscribeResponse>();

        internal readonly Subject<Message<MarketCandlestickTick>> MarketCandlestickSubject =
            new Subject<Message<MarketCandlestickTick>>();

        internal readonly Subject<Message<MarketDepthTick>> MarketDepthSubject = new Subject<Message<MarketDepthTick>>();

        public IObservable<string> UnhandledMessageStream => UnhandledMessageSubject.AsObservable();
        public IObservable<SubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
        public IObservable<Message<MarketCandlestickTick>> MarketCandlestickStream => MarketCandlestickSubject.AsObservable();
        public IObservable<Message<MarketDepthTick>> MarketDepthStream => MarketDepthSubject.AsObservable();
    }
}