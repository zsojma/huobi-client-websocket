using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiMarketByPriceClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<SubscribeResponse> SubscribeResponseSubject = new();
        internal readonly Subject<UnsubscribeResponse> UnsubscribeResponseSubject = new();
        
        internal readonly Subject<MarketByPriceUpdateMessage> MarketByPriceUpdateSubject = new();
        internal readonly Subject<MarketByPricePullResponse> MarketByPricePullSubject = new();
        
        public IObservable<SubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
        public IObservable<UnsubscribeResponse> UnsubscribeResponseStream => UnsubscribeResponseSubject.AsObservable();
        
        public IObservable<MarketByPriceUpdateMessage> MarketByPriceUpdateStream => MarketByPriceUpdateSubject.AsObservable();
        public IObservable<MarketByPricePullResponse> MarketByPricePullStream => MarketByPricePullSubject.AsObservable();
    }
}