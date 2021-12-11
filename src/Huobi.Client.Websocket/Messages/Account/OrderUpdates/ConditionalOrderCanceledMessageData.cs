using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates;

public class ConditionalOrderCanceledMessageData
{
    [JsonConstructor]
    public ConditionalOrderCanceledMessageData(
        OrderEventType eventType,
        OrderStatus orderStatus,
        string symbol,
        string clientOrderId,
        OrderSide orderSide,
        DateTimeOffset orderTriggerTime)
    {
        EventType = eventType;
        Symbol = symbol;
        ClientOrderId = clientOrderId;
        OrderSide = orderSide;
        OrderStatus = orderStatus;
        OrderTriggerTime = orderTriggerTime;
    }

    public OrderEventType EventType { get; }
    public string Symbol { get; }
    public string ClientOrderId { get; }
    public OrderSide OrderSide { get; }
    public OrderStatus OrderStatus { get; }

    [JsonProperty("lastActTime")]
    public DateTimeOffset OrderTriggerTime { get; }
}