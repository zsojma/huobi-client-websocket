using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderSubmittedData
    {
        [JsonConstructor]
        public OrderSubmittedData(
            string eventTypeStr,
            string symbol,
            long accountId,
            long orderId,
            string clientOrderId,
            string orderSource,
            string orderPrice,
            string orderSize,
            string orderValue,
            string orderTypeStr,
            string orderStatusStr,
            long orderCreationTime)
        {
            EventTypeStr = eventTypeStr;
            Symbol = symbol;
            AccountId = accountId;
            OrderId = orderId;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            OrderTypeStr = orderTypeStr;
            OrderStatusStr = orderStatusStr;
            OrderCreationTime = orderCreationTime;
        }


        [JsonProperty("eventType")]
        public string EventTypeStr { get; }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        public string Symbol { get; }
        public long AccountId { get; }
        public long OrderId { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public string OrderPrice { get; }
        public string OrderSize { get; }
        public string OrderValue { get; }

        [JsonProperty("type")]
        public string OrderTypeStr { get; }

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        [JsonProperty("orderStatus")]
        public string OrderStatusStr { get; }

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        public long OrderCreationTime { get; }
    }
}