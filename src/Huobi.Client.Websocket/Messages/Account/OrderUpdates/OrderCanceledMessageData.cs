using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderCanceledMessageData
    {
        [JsonConstructor]
        public OrderCanceledMessageData(
            string eventTypeStr,
            string orderTypeStr,
            string orderStatusStr,
            long lastActivityTimeMs,
            string symbol,
            long orderId,
            string clientOrderId,
            string orderSource,
            decimal orderPrice,
            decimal orderSize,
            decimal orderValue,
            string remainingAmount,
            string accumulativeAmount)
        {
            EventTypeStr = eventTypeStr;
            OrderTypeStr = orderTypeStr;
            OrderStatusStr = orderStatusStr;
            LastActivityTimeMs = lastActivityTimeMs;

            Symbol = symbol;
            OrderId = orderId;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            RemainingAmount = remainingAmount;
            AccumulativeAmount = accumulativeAmount;
        }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        [JsonIgnore]
        public DateTimeOffset LastActivityTime => DateTimeOffset.FromUnixTimeMilliseconds(LastActivityTimeMs);

        public string Symbol { get; }
        public long OrderId { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal OrderValue { get; }

        [JsonProperty("remainAmt")]
        public string RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public string AccumulativeAmount { get; }

        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("type")]
        internal string OrderTypeStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }

        [JsonProperty("lastActTime")]
        internal long LastActivityTimeMs { get; }
    }
}