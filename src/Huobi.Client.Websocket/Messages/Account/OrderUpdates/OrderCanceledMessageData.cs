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
            string symbol,
            long orderId,
            string clientOrderId,
            string orderSource,
            string orderPrice,
            string orderSize,
            string orderValue,
            string remainingAmount,
            string accumulativeAmount,
            long lastActivityTime)
        {
            EventTypeStr = eventTypeStr;
            OrderTypeStr = orderTypeStr;
            OrderStatusStr = orderStatusStr;

            Symbol = symbol;
            OrderId = orderId;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            RemainingAmount = remainingAmount;
            AccumulativeAmount = accumulativeAmount;
            LastActivityTime = lastActivityTime;
        }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        public string Symbol { get; }
        public long OrderId { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public string OrderPrice { get; }
        public string OrderSize { get; }
        public string OrderValue { get; }

        [JsonProperty("remainAmt")]
        public string RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public string AccumulativeAmount { get; }

        [JsonProperty("lastActTime")]
        public long LastActivityTime { get; }


        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("type")]
        internal string OrderTypeStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }
    }
}