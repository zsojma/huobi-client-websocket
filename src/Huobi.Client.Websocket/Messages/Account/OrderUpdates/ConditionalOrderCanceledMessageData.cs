using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Messages.MarketData.Values;
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
            string symbol,
            string clientOrderId,
            DateTimeOffset orderTriggerTime)
        {
            EventTypeStr = eventTypeStr;
            OrderSideStr = orderSideStr;
            OrderStatusStr = orderStatusStr;

            Symbol = symbol;
            ClientOrderId = clientOrderId;
            OrderTriggerTime = orderTriggerTime;
        }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderSide OrderSide => OrderSideHelper.FromMessageValue(OrderSideStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        public string Symbol { get; }
        public string ClientOrderId { get; }

        [JsonProperty("lastActTime")]
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset OrderTriggerTime { get; }

        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("orderSide")]
        internal string OrderSideStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }
    }
}