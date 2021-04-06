using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderCanceledData
    {
        [JsonConstructor]
        public OrderCanceledData(
            string eventTypeStr,
            string symbol,
            long orderId,
            string orderTypeStr,
            string clientOrderId,
            string orderSource,
            string orderPrice,
            string orderSize,
            string orderValue,
            string orderStatusStr,
            string remainingAmount,
            string accumulativeAmount,
            long lastActivityTime)
        {
            EventTypeStr = eventTypeStr;
            Symbol = symbol;
            OrderId = orderId;
            OrderTypeStr = orderTypeStr;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            OrderStatusStr = orderStatusStr;
            RemainingAmount = remainingAmount;
            AccumulativeAmount = accumulativeAmount;
            LastActivityTime = lastActivityTime;
        }

        [JsonProperty("eventType")]
        public string EventTypeStr { get; }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        public string Symbol { get; }
        public long OrderId { get; }

        [JsonProperty("type")]
        public string OrderTypeStr { get; }

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public string OrderPrice { get; }
        public string OrderSize { get; }
        public string OrderValue { get; }

        [JsonProperty("orderStatus")]
        public string OrderStatusStr { get; }

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        public long OrderCreationTime { get; }

        [JsonProperty("remainAmt")]
        public string RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public string AccumulativeAmount { get; }

        [JsonProperty("lastActTime")]
        public long LastActivityTime { get; }
    }
}