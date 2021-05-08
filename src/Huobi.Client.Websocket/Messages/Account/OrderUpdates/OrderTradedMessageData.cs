using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderTradedMessageData
    {
        [JsonConstructor]
        public OrderTradedMessageData(
            OrderEventType eventType,
            string symbol,
            decimal tradePrice,
            decimal tradeVolume,
            long orderId,
            OrderType type,
            string clientOrderId,
            string orderSource,
            decimal orderPrice,
            decimal orderSize,
            decimal orderValue,
            long tradeId,
            DateTimeOffset tradeTime,
            bool aggressor,
            OrderStatus orderStatus,
            decimal remainingAmount,
            decimal accumulativeAmount)
        {
            EventType = eventType;
            Symbol = symbol;
            TradePrice = tradePrice;
            TradeVolume = tradeVolume;
            OrderId = orderId;
            Type = type;
            ClientOrderId = clientOrderId;
            OrderSource = orderSource;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            TradeId = tradeId;
            TradeTime = tradeTime;
            Aggressor = aggressor;
            OrderStatus = orderStatus;
            RemainingAmount = remainingAmount;
            AccumulativeAmount = accumulativeAmount;
        }

        public OrderEventType EventType { get; }
        public string Symbol { get; }
        public decimal TradePrice { get; }
        public decimal TradeVolume { get; }
        public long OrderId { get; }
        public OrderType Type { get; }
        public string ClientOrderId { get; }
        public string OrderSource { get; }
        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal OrderValue { get; }
        public long TradeId { get; }
        public DateTimeOffset TradeTime { get; }
        public bool Aggressor { get; }
        public OrderStatus OrderStatus { get; }

        [JsonProperty("remainAmt")]
        public decimal RemainingAmount { get; }

        [JsonProperty("execAmt")]
        public decimal AccumulativeAmount { get; }
    }
}