using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.TradeDetails
{
    public class TradeDetailsMessageData
    {
        [JsonConstructor]
        public TradeDetailsMessageData(
            string eventTypeStr,
            string orderSideStr,
            string orderTypeStr,
            string orderStatusStr,
            string symbol,
            long orderId,
            decimal tradePrice,
            decimal tradeVolume,
            bool aggressor,
            long tradeId,
            DateTimeOffset tradeTime,
            decimal transactionFee,
            string feeCurrency,
            decimal feeDeduct,
            string feeDeductType,
            long accountId,
            string source,
            decimal orderPrice,
            decimal orderSize,
            decimal orderValue,
            string clientOrderId,
            decimal stopPrice,
            DateTimeOffset orderCreateTime,
            string @operator)
        {
            EventTypeStr = eventTypeStr;
            OrderSideStr = orderSideStr;
            OrderTypeStr = orderTypeStr;
            OrderStatusStr = orderStatusStr;

            Symbol = symbol;
            OrderId = orderId;
            TradePrice = tradePrice;
            TradeVolume = tradeVolume;
            Aggressor = aggressor;
            TradeId = tradeId;
            TradeTime = tradeTime;
            TransactionFee = transactionFee;
            FeeCurrency = feeCurrency;
            FeeDeduct = feeDeduct;
            FeeDeductType = feeDeductType;
            AccountId = accountId;
            Source = source;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            OrderValue = orderValue;
            ClientOrderId = clientOrderId;
            StopPrice = stopPrice;
            OrderCreateTime = orderCreateTime;
            Operator = @operator;
        }

        [JsonIgnore]
        public TradeEventType EventType => TradeEventTypeHelper.FromMessageValue(EventTypeStr);

        [JsonIgnore]
        public OrderSide OrderSide => OrderSideHelper.FromMessageValue(OrderSideStr);

        [JsonIgnore]
        public OrderType OrderType => OrderTypeHelper.FromMessageValue(OrderTypeStr);

        [JsonIgnore]
        public OrderStatus OrderStatus => OrderStatusHelper.FromMessageValue(OrderStatusStr);

        public string Symbol { get; }
        public long OrderId { get; }
        public decimal TradePrice { get; }
        public decimal TradeVolume { get; }
        public bool Aggressor { get; }
        public long TradeId { get; }

        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset TradeTime { get; }

        [JsonProperty("transactFee")]
        public decimal TransactionFee { get; }

        public string FeeCurrency { get; }
        public decimal FeeDeduct { get; }
        public string FeeDeductType { get; }
        public long AccountId { get; }
        public string Source { get; }
        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal OrderValue { get; }
        public string ClientOrderId { get; }
        public decimal StopPrice { get; }

        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset OrderCreateTime { get; }

        public string Operator { get; }


        [JsonProperty("eventType")]
        internal string EventTypeStr { get; }

        [JsonProperty("orderSide")]
        internal string OrderSideStr { get; }

        [JsonProperty("orderType")]
        internal string OrderTypeStr { get; }

        [JsonProperty("orderStatus")]
        internal string OrderStatusStr { get; }
    }
}