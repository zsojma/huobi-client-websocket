using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.Account.AccountUpdates;
using Huobi.Client.Websocket.Messages.Account.OrderUpdates;
using Huobi.Client.Websocket.Messages.Account.TradeDetails;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiFuturesClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<AuthenticationResponse> AuthenticationResponseSubject = new();
        internal readonly Subject<AccountSubscribeResponse> SubscribeResponseSubject = new();

        internal readonly Subject<ConditionalOrderTriggerFailureMessage> ConditionalOrderTriggerFailureMessageSubject = new();
        internal readonly Subject<ConditionalOrderCanceledMessage> ConditionalOrderCanceledMessageSubject = new();
        internal readonly Subject<OrderSubmittedMessage> OrderSubmittedMessageSubject = new();
        internal readonly Subject<OrderTradedMessage> OrderTradedMessageSubject = new();
        internal readonly Subject<OrderCanceledMessage> OrderCanceledMessageSubject = new();

        internal readonly Subject<TradeDetailsMessage> TradeDetailsMessageSubject = new();

        internal readonly Subject<AccountUpdateMessage> AccountUpdateMessageSubject = new();
        internal readonly Subject<AccountUpdateMessage> FuturesLiquidationSubject = new();

        public IObservable<AuthenticationResponse> AuthenticationResponseStream => AuthenticationResponseSubject.AsObservable();
        public IObservable<AccountSubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();

        public IObservable<ConditionalOrderTriggerFailureMessage> ConditionalOrderTriggerFailureMessageStream => ConditionalOrderTriggerFailureMessageSubject.AsObservable();
        public IObservable<ConditionalOrderCanceledMessage> ConditionalOrderCanceledMessageStream => ConditionalOrderCanceledMessageSubject.AsObservable();
        public IObservable<OrderSubmittedMessage> OrderSubmittedMessageStream => OrderSubmittedMessageSubject.AsObservable();
        public IObservable<OrderTradedMessage> OrderTradedMessageStream => OrderTradedMessageSubject.AsObservable();
        public IObservable<OrderCanceledMessage> OrderCanceledMessageStream => OrderCanceledMessageSubject.AsObservable();

        public IObservable<TradeDetailsMessage> TradeDetailsMessageStream => TradeDetailsMessageSubject.AsObservable();

        public IObservable<AccountUpdateMessage> AccountUpdateMessageStream => AccountUpdateMessageSubject.AsObservable();
        public IObservable<AccountUpdateMessage> FuturesLiquidationMessageStream => FuturesLiquidationSubject.AsObservable();

    }
}