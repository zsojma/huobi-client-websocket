using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.TradeDetails;

public class TradeDetailsMessageData
{
    [JsonConstructor]
    public TradeDetailsMessageData(
        TradeEventType eventType,
        string symbol,
        long orderId,
        decimal tradePrice,
        decimal tradeVolume,
        OrderSide orderSide,
        OrderType orderType,
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
        string @operator,
        DateTimeOffset orderCreateTime,
        OrderStatus orderStatus)
    {
        EventType = eventType;
        Symbol = symbol;
        OrderId = orderId;
        TradePrice = tradePrice;
        TradeVolume = tradeVolume;
        OrderSide = orderSide;
        OrderType = orderType;
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
        OrderStatus = orderStatus;
    }

    public TradeEventType EventType { get; }
    public string Symbol { get; }
    public long OrderId { get; }
    public decimal TradePrice { get; }
    public decimal TradeVolume { get; }
    public OrderSide OrderSide { get; }
    public OrderType OrderType { get; }
    public bool Aggressor { get; }
    public long TradeId { get; }
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
    public string Operator { get; }
    public DateTimeOffset OrderCreateTime { get; }
    public OrderStatus OrderStatus { get; }
}