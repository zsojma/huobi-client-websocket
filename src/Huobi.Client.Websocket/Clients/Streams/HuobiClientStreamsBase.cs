using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.MarketData;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiClientStreamsBase
    {
        internal readonly Subject<string> UnhandledMessageSubject = new();
        internal readonly Subject<ErrorMessage> ErrorMessageSubject = new();
        internal readonly Subject<AuthErrorMessage> AuthErrorMessageSubject = new();
        internal readonly Subject<PingRequest> PingMessageSubject = new();
        internal readonly Subject<AuthPingRequest> PingAuthMessageSubject = new();

        public IObservable<string> UnhandledMessageStream => UnhandledMessageSubject.AsObservable();
        public IObservable<ErrorMessage> ErrorMessageStream => ErrorMessageSubject.AsObservable();
        public IObservable<AuthErrorMessage> AuthErrorMessageStream => AuthErrorMessageSubject.AsObservable();
        public IObservable<PingRequest> PingMessageStream => PingMessageSubject.AsObservable();
        public IObservable<AuthPingRequest> PingAuthMessageStream => PingAuthMessageSubject.AsObservable();
    }
}