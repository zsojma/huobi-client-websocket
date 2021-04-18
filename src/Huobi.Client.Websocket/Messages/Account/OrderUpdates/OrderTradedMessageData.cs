using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Messages.MarketData.Values;
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
            decimal tradePrice,
            decimal tradeVolume,
            long orderId,
            string clientOrderId,
            string orderSource,
            decimal orderPrice,
            decimal orderSize,
            decimal orderValue,
            long tradeId,
            DateTimeOffset tradeTime,
            bool aggressor,
            decimal remainingAmount,
            decimal accumulativeAmount)
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
        public decimal TradePrice { get; }
        public decimal TradeVolume { get; }
        public long OrderId { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal OrderValue { get; }
        public long TradeId { get; }

        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset TradeTime { get; }

        public bool Aggressor { get; }

        [JsonProperty("remainAmt")]
        public decimal RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public decimal AccumulativeAmount { get; }


        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("type")]
        internal string OrderTypeStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }
    }
}