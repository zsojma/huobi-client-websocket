using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.Account.AccountUpdates;
using Huobi.Client.Websocket.Messages.Account.OrderUpdates;
using Huobi.Client.Websocket.Messages.Account.TradeDetails;

namespace Huobi.Client.Websocket.Clients.Streams;

public class HuobiAccountClientStreams : HuobiClientStreamsBase
{
    public readonly Subject<AuthenticationResponse> AuthenticationResponseStream = new();
    public readonly Subject<AccountSubscribeResponse> SubscribeResponseStream = new();
        
    public readonly Subject<ConditionalOrderTriggerFailureMessage> ConditionalOrderTriggerFailureMessageStream = new();
    public readonly Subject<ConditionalOrderCanceledMessage> ConditionalOrderCanceledMessageStream = new();
    public readonly Subject<OrderSubmittedMessage> OrderSubmittedMessageStream = new();
    public readonly Subject<OrderTradedMessage> OrderTradedMessageStream = new();
    public readonly Subject<OrderCanceledMessage> OrderCanceledMessageStream = new();

    public readonly Subject<TradeDetailsMessage> TradeDetailsMessageStream = new();

    public readonly Subject<AccountUpdateMessage> AccountUpdateMessageStream = new();
}