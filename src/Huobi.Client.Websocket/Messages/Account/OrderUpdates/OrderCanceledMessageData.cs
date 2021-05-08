using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderCanceledMessageData
    {
        [JsonConstructor]
        public OrderCanceledMessageData(
            OrderEventType eventType,
            string symbol,
            long orderId,
            OrderType type,
            string clientOrderId,
            string orderSource,
            decimal orderPrice,
            decimal orderSize,
            decimal orderValue,
            OrderStatus orderStatus,
            string remainingAmount,
            string accumulativeAmount,
            DateTimeOffset lastActivityTime)
        {
            EventType = eventType;
            Symbol = symbol;
            OrderId = orderId;
            Type = type;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            OrderStatus = orderStatus;
            RemainingAmount = remainingAmount;
            AccumulativeAmount = accumulativeAmount;
            LastActivityTime = lastActivityTime;
        }

        public OrderEventType EventType { get; }
        public string Symbol { get; }
        public long OrderId { get; }
        public OrderType Type { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal OrderValue { get; }
        public OrderStatus OrderStatus { get; }

        [JsonProperty("remainAmt")]
        public string RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public string AccumulativeAmount { get; }

        [JsonProperty("lastActTime")]
        public DateTimeOffset LastActivityTime { get; }
    }
}