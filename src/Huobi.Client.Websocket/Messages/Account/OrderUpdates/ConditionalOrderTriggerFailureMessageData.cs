using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderTriggerFailureMessageData
    {
        [JsonConstructor]
        public ConditionalOrderTriggerFailureMessageData(
            string eventTypeStr,
            string orderSideStr,
            string orderStatusStr,
            long orderTriggeringFailureTimeMs,
            string symbol,
            string clientOrderId,
            int errorCode,
            string errorMessage)
        {
            EventTypeStr = eventTypeStr;
            OrderSideStr = orderSideStr;
            OrderStatusStr = orderStatusStr;
            OrderTriggeringFailureTimeMs = orderTriggeringFailureTimeMs;
            
            Symbol = symbol;
            ClientOrderId = clientOrderId;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderSide OrderSide => OrderSideHelper.FromMessageValue(OrderSideStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        [JsonIgnore]
        public DateTimeOffset OrderTriggeringFailureTime => DateTimeOffset.FromUnixTimeMilliseconds(OrderTriggeringFailureTimeMs);

        public string Symbol { get; }
        public string ClientOrderId { get; }

        [JsonProperty("errCode")]
        public int ErrorCode { get; }

        [JsonProperty("errMessage")]
        public string ErrorMessage { get; }

        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("orderSide")]
        internal string OrderSideStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }

        [JsonProperty("lastActTime")]
        internal long OrderTriggeringFailureTimeMs { get; }
    }
}