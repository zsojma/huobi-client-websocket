using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.Account.Subscription;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiAccountClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<AuthenticationResponse> AuthenticationResponseSubject = new();
        internal readonly Subject<AccountSubscribeResponse> SubscribeResponseSubject = new();

        public IObservable<AuthenticationResponse> AuthenticationResponseStream => AuthenticationResponseSubject.AsObservable();
        public IObservable<AccountSubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
    }
}