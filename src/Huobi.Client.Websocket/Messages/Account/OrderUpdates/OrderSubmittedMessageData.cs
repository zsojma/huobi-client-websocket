using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates;

public class OrderSubmittedMessageData
{
    [JsonConstructor]
    public OrderSubmittedMessageData(
        OrderEventType eventType,
        string symbol,
        long accountId,
        long orderId,
        string clientOrderId,
        string orderSource,
        decimal orderPrice,
        decimal orderSize,
        decimal orderValue,
        OrderType type,
        OrderStatus orderStatus,
        DateTimeOffset orderCreateTime)
    {
        EventType = eventType;
        Symbol = symbol;
        AccountId = accountId;
        OrderId = orderId;
        ClientOrderId = clientOrderId;
        OrderSource = orderSource;
        OrderPrice = orderPrice;
        OrderSize = orderSize;
        OrderValue = orderValue;
        Type = type;
        OrderStatus = orderStatus;
        OrderCreateTime = orderCreateTime;
    }

    public OrderEventType EventType { get; }
    public string Symbol { get; }
    public long AccountId { get; }
    public long OrderId { get; }
    public string ClientOrderId { get; }
    public string OrderSource { get; }
    public decimal OrderPrice { get; }
    public decimal OrderSize { get; }
    public decimal OrderValue { get; }
    public OrderType Type { get; }
    public OrderStatus OrderStatus { get; }
    public DateTimeOffset OrderCreateTime { get; }
}