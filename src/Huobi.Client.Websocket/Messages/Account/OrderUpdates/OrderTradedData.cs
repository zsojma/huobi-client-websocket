using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderTradedData
    {
        [JsonConstructor]
        public OrderTradedData(
            string eventTypeStr,
            string symbol,
            string tradePrice,
            string tradeVolume,
            long orderId,
            string orderTypeStr,
            string clientOrderId,
            string orderSource,
            string orderPrice,
            string orderSize,
            string orderValue,
            long tradeId,
            long tradeTime,
            bool aggressor,
            string orderStatusStr,
            string remainingAmount,
            string accumulativeAmount)
        {
            EventTypeStr = eventTypeStr;
            Symbol = symbol;
            TradePrice = tradePrice;
            TradeVolume = tradeVolume;
            OrderId = orderId;
            OrderTypeStr = orderTypeStr;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            TradeId = tradeId;
            TradeTime = tradeTime;
            Aggressor = aggressor;
            OrderStatusStr = orderStatusStr;
            RemainingAmount = remainingAmount;
            AccumulativeAmount = accumulativeAmount;
        }


        [JsonProperty("eventType")]
        public string EventTypeStr { get; }

        [JsonIgnore]
        public OrderEventType EventType => OrderEventTypeHelper.FromMessageValue(EventTypeStr);

        public string Symbol { get; }
        public string TradePrice { get; }
        public string TradeVolume { get; }
        public long OrderId { get; }

        [JsonProperty("type")]
        public string OrderTypeStr { get; }

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public string OrderPrice { get; }
        public string OrderSize { get; }
        public string OrderValue { get; }
        public long TradeId { get; }
        public long TradeTime { get; }
        public bool Aggressor { get; }

        [JsonProperty("orderStatus")]
        public string OrderStatusStr { get; }

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        [JsonProperty("remainAmt")]
        public string RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public string AccumulativeAmount { get; }
    }
}