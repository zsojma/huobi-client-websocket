using Huobi.Client.Websocket.Messages.Account.Values;
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
            string tradePrice,
            string tradeVolume,
            bool aggressor,
            long tradeId,
            long tradeTime,
            string transactionFee,
            string feeCurrency,
            string feeDeduct,
            string feeDeductType,
            long accountId,
            string source,
            string orderPrice,
            string orderSize,
            string orderValue,
            string clientOrderId,
            string stopPrice,
            string @operator,
            long orderCreateTime)
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
            Operator = @operator;
            OrderCreateTime = orderCreateTime;
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
        public string TradePrice { get; }
        public string TradeVolume { get; }
        public bool Aggressor { get; }
        public long TradeId { get; }
        public long TradeTime { get; }

        [JsonProperty("transacFee")]
        public string TransactionFee { get; }

        public string FeeCurrency { get; }
        public string FeeDeduct { get; }
        public string FeeDeductType { get; }
        public long AccountId { get; }
        public string Source { get; }
        public string OrderPrice { get; }
        public string OrderSize { get; }
        public string OrderValue { get; }
        public string ClientOrderId { get; }
        public string StopPrice { get; }
        public string Operator { get; }
        public long OrderCreateTime { get; }

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