using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderSubmittedMessageData
    {
        [JsonConstructor]
        public OrderSubmittedMessageData(
            string eventTypeStr,
            string orderTypeStr,
            string orderStatusStr,
            long orderCreateTimeMs,
            string symbol,
            long accountId,
            long orderId,
            string clientOrderId,
            string orderSource,
            decimal orderPrice,
            decimal orderSize,
            decimal orderValue)
        {
            EventTypeStr = eventTypeStr;
            OrderTypeStr = orderTypeStr;
            OrderStatusStr = orderStatusStr;
            OrderCreateTimeMs = orderCreateTimeMs;

            Symbol = symbol;
            AccountId = accountId;
            OrderId = orderId;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
        }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        [JsonIgnore]
        public DateTimeOffset OrderCreateTime => DateTimeOffset.FromUnixTimeMilliseconds(OrderCreateTimeMs);

        public string Symbol { get; }
        public long AccountId { get; }
        public long OrderId { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal OrderValue { get; }

        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("type")]
        internal string OrderTypeStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }

        [JsonProperty("orderCreateTime")]
        internal long OrderCreateTimeMs { get; }
    }
}