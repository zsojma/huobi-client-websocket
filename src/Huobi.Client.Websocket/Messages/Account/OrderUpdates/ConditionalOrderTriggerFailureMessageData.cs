using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderTriggerFailureMessageData
    {
        [JsonConstructor]
        public ConditionalOrderTriggerFailureMessageData(
            OrderEventType eventType,
            string symbol,
            string clientOrderId,
            OrderSide orderSide,
            OrderStatus orderStatus,
            int errorCode,
            string errorMessage,
            DateTimeOffset orderTriggeringFailureTime)
        {
            EventType = eventType;
            Symbol = symbol;
            ClientOrderId = clientOrderId;
            OrderSide = orderSide;
            OrderStatus = orderStatus;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            OrderTriggeringFailureTime = orderTriggeringFailureTime;
        }
        
        public OrderEventType EventType { get; }
        public string Symbol { get; }
        public string ClientOrderId { get; }
        public OrderSide OrderSide { get; }
        public OrderStatus OrderStatus { get; }

        [JsonProperty("errCode")]
        public int ErrorCode { get; }

        [JsonProperty("errMessage")]
        public string ErrorMessage { get; }

        [JsonProperty("lastActTime")]
        public DateTimeOffset OrderTriggeringFailureTime { get; }
    }
}