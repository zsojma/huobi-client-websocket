using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiGenericClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<string> ResponseMessageSubject = new();
        public IObservable<string> ResponseMessageStream => ResponseMessageSubject.AsObservable();
    }
}