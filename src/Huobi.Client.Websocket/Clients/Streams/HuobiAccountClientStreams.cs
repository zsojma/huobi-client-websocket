using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiAccountClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<AuthenticationResponse> AuthenticationResponseSubject = new();

        public IObservable<AuthenticationResponse> AuthenticationResponseStream => AuthenticationResponseSubject.AsObservable();
    }
}