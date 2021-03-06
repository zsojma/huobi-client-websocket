using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Huobi.Client.Websocket.Client
{
    public class HuobiClientStreams
    {
        internal readonly Subject<string> UnhandledMessageSubject = new Subject<string>();
        
        public IObservable<string> UnhandledMessageStream => UnhandledMessageSubject.AsObservable();
    }
}