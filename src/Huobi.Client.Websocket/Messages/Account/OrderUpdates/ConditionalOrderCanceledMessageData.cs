using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderCanceledMessageData
    {
        [JsonConstructor]
        public ConditionalOrderCanceledMessageData(
            string eventTypeStr,
            string orderSideStr,
            string orderStatusStr,
            long orderTriggerTimeMs,
            string symbol,
            string clientOrderId)
        {
            EventTypeStr = eventTypeStr;
            OrderSideStr = orderSideStr;
            OrderStatusStr = orderStatusStr;
            OrderTriggerTimeMs = orderTriggerTimeMs;

            Symbol = symbol;
            ClientOrderId = clientOrderId;
        }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderSide OrderSide => OrderSideHelper.FromMessageValue(OrderSideStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        [JsonIgnore]
        public DateTimeOffset OrderTriggerTime => DateTimeOffset.FromUnixTimeMilliseconds(OrderTriggerTimeMs);

        public string Symbol { get; }
        public string ClientOrderId { get; }

        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("orderSide")]
        internal string OrderSideStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }

        [JsonProperty("lastActTime")]
        internal long OrderTriggerTimeMs { get; }
    }
}