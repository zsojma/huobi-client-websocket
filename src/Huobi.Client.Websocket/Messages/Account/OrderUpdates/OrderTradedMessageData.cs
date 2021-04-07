using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderTradedMessageData
    {
        [JsonConstructor]
        public OrderTradedMessageData(
            string eventTypeStr,
            string orderTypeStr,
            string orderStatusStr,
            string symbol,
            string tradePrice,
            string tradeVolume,
            long orderId,
            string clientOrderId,
            string orderSource,
            string orderPrice,
            string orderSize,
            string orderValue,
            long tradeId,
            long tradeTime,
            bool aggressor,
            string remainingAmount,
            string accumulativeAmount)
        {
            EventTypeStr = eventTypeStr;
            OrderTypeStr = orderTypeStr;
            OrderStatusStr = orderStatusStr;

            Symbol = symbol;
            TradePrice = tradePrice;
            TradeVolume = tradeVolume;
            OrderId = orderId;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            TradeId = tradeId;
            TradeTime = tradeTime;
            Aggressor = aggressor;
            RemainingAmount = remainingAmount;
            AccumulativeAmount = accumulativeAmount;
        }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        public string Symbol { get; }
        public string TradePrice { get; }
        public string TradeVolume { get; }
        public long OrderId { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public string OrderPrice { get; }
        public string OrderSize { get; }
        public string OrderValue { get; }
        public long TradeId { get; }
        public long TradeTime { get; }
        public bool Aggressor { get; }

        [JsonProperty("remainAmt")]
        public string RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public string AccumulativeAmount { get; }


        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("type")]
        internal string OrderTypeStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }
    }
}