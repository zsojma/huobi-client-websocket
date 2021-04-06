using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderTriggerFailureData
    {
        [JsonConstructor]
        public ConditionalOrderTriggerFailureData(
            string eventTypeStr,
            string symbol,
            string clientOrderId,
            string orderSideStr,
            string orderStatusStr,
            int errorCode,
            string errorMessage,
            long orderTriggeringFailureTime)
        {
            EventTypeStr = eventTypeStr;
            Symbol = symbol;
            ClientOrderId = clientOrderId;
            OrderSideStr = orderSideStr;
            OrderStatusStr = orderStatusStr;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            OrderTriggeringFailureTime = orderTriggeringFailureTime;
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

        [JsonProperty("errCode")]
        public int ErrorCode { get; }

        [JsonProperty("errMessage")]
        public string ErrorMessage { get; }

        [JsonProperty("lastActTime")]
        public long OrderTriggeringFailureTime { get; }
    }
}