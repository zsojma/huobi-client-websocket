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
        internal readonly Subject<PingRequest> PingMessageSubject = new();
        internal readonly Subject<AccountErrorMessage> AccountErrorMessageSubject = new();
        internal readonly Subject<AccountPingRequest> AccountPingMessageSubject = new();

        public IObservable<string> UnhandledMessageStream => UnhandledMessageSubject.AsObservable();
        public IObservable<ErrorMessage> ErrorMessageStream => ErrorMessageSubject.AsObservable();
        public IObservable<PingRequest> PingMessageStream => PingMessageSubject.AsObservable();
        public IObservable<AccountErrorMessage> AccountErrorMessageStream => AccountErrorMessageSubject.AsObservable();
        public IObservable<AccountPingRequest> AccountPingMessageStream => AccountPingMessageSubject.AsObservable();
    }
}