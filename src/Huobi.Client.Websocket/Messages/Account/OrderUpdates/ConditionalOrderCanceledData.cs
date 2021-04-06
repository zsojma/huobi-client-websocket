using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderCanceledData
    {
        [JsonConstructor]
        public ConditionalOrderCanceledData(
            string eventTypeStr,
            string symbol,
            string clientOrderId,
            string orderSideStr,
            string orderStatusStr,
            long orderTriggerTime)
        {
            EventTypeStr = eventTypeStr;
            Symbol = symbol;
            ClientOrderId = clientOrderId;
            OrderSideStr = orderSideStr;
            OrderStatusStr = orderStatusStr;
            OrderTriggerTime = orderTriggerTime;
        }

        [JsonProperty("eventType")]
        public string EventTypeStr { get; }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        public string Symbol { get; }
        public string ClientOrderId { get; }

        [JsonProperty("orderSide")]
        public string OrderSideStr { get; }

        [JsonIgnore]
        public OrderSide OrderSide => OrderSideHelper.FromMessageValue(OrderSideStr);

        [JsonProperty("orderStatus")]
        public string OrderStatusStr { get; }

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        [JsonProperty("lastActTime")]
        public long OrderTriggerTime { get; }
    }
}