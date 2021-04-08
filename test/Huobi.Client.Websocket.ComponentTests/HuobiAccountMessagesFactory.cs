using System;
using Huobi.Client.Websocket.Messages.Account.Values;

namespace Huobi.Client.Websocket.ComponentTests
{
    public static class HuobiAccountMessagesFactory
    {
        public static string CreateAuthenticationResponseMessage()
        {
            var message = @"{
    ""action"": ""req"",
    ""code"": 200,
    ""ch"": ""auth"",
    ""data"": {}
}";
            return message;
        }

        public static string CreateSubscribeResponseMessage()
        {
            var message = @"{
    ""action"": ""sub"",
    ""code"": 200,
    ""ch"": ""orders#btcusdt"",
    ""data"": {}
}";
            return message;
        }

        public static string CreateErrorMessage()
        {
            var message = @"{
    ""code"": 2002,
    ""ch"": ""auth"",
    ""message"": ""auth.fail""
}";
            return message;
        }

        public static string CreateSubscribeErrorMessage(AccountSubscriptionType subscriptionType)
        {
            var message = @"{
    ""action"":""sub"",
    ""code"":2002,
    ""ch"":""" + subscriptionType.ToTopicId() + @"#btcusdt"",
    ""message"":""invalid.auth.state""
}";
            return message;
        }

        public static string CreateConditionalOrderTriggeringFailureMessage(OrderSide orderSide, DateTimeOffset lastActTime)
        {
            var message = @"{
    ""action"":""push"",
    ""ch"":""orders#btcusdt"",
    ""data"":
    {
        ""orderSide"":""" + orderSide.ToMessageValue() + @""",
        ""lastActTime"":" + lastActTime.ToUnixTimeMilliseconds() + @",
        ""clientOrderId"":""abc123"",
        ""orderStatus"":""rejected"",
        ""symbol"":""btcusdt"",
        ""eventType"":""trigger"",
        ""errCode"": 2002,
        ""errMessage"":""invalid.client.order.id (NT)""
    }
}";
            return message;
        }

        public static string CreateConditionalOrderCanceledMessage(OrderSide orderSide, DateTimeOffset lastActTime)
        {
            var message = @"{
    ""action"":""push"",
    ""ch"":""orders#btcusdt"",
    ""data"":
    {
        ""orderSide"":""" + orderSide.ToMessageValue() + @""",
        ""lastActTime"":" + lastActTime.ToUnixTimeMilliseconds() + @",
        ""clientOrderId"":""abc123"",
        ""orderStatus"":""canceled"",
        ""symbol"":""btcusdt"",
        ""eventType"":""deletion""
    }
}";
            return message;
        }

        public static string CreateOrderSubmittedMessage(OrderType orderType, DateTimeOffset createTime)
        {
            var message = @"{
    ""action"":""push"",
    ""ch"":""orders#btcusdt"",
    ""data"":
    {
        ""orderSize"":""2.000000000000000000"",
        ""orderCreateTime"":" + createTime.ToUnixTimeMilliseconds() + @",
        ""accountId"":992701,
        ""orderPrice"":""77.000000000000000000"",
        ""type"":""" + orderType.ToMessageValue() + @""",
        ""orderId"":27163533,
        ""clientOrderId"":""abc123"",
        ""orderSource"":""spot-api"",
        ""orderStatus"":""submitted"",
        ""symbol"":""btcusdt"",
        ""eventType"":""creation""

    }
}";
            return message;
        }

        public static string CreateOrderTradedMessage(OrderStatus orderStatus, OrderType orderType, DateTimeOffset tradeTime)
        {
            var message = @"{
    ""action"":""push"",
    ""ch"":""orders#btcusdt"",
    ""data"":
    {
        ""tradePrice"":""76.000000000000000000"",
        ""tradeVolume"":""1.013157894736842100"",
        ""tradeId"":301,
        ""tradeTime"":" + tradeTime.ToUnixTimeMilliseconds() + @",
        ""aggressor"":true,
        ""remainAmt"":""0.000000000000000400000000000000000000"",
        ""execAmt"":""2"",
        ""orderId"":27163536,
        ""type"":""" + orderType.ToMessageValue() + @""",
        ""clientOrderId"":""abc123"",
        ""orderSource"":""spot-api"",
        ""orderPrice"":""15000"",
        ""orderSize"":""0.01"",
        ""orderStatus"":""" + orderStatus.ToMessageValue() + @""",
        ""symbol"":""btcusdt"",
        ""eventType"":""trade""
    }
}";
            return message;
        }

        public static string CreateOrderCanceledMessage(OrderType orderType)
        {
            var message = @"{
    ""action"":""push"",
    ""ch"":""orders#btcusdt"",
    ""data"":
    {
        ""lastActTime"":1583853475406,
        ""remainAmt"":""2.000000000000000000"",
        ""execAmt"":""2"",
        ""orderId"":27163533,
        ""type"":""" + orderType.ToMessageValue() + @""",
        ""clientOrderId"":""abc123"",
        ""orderSource"":""spot-api"",
        ""orderPrice"":""15000"",
        ""orderSize"":""0.01"",
        ""orderStatus"":""canceled"",
        ""symbol"":""btcusdt"",
        ""eventType"":""cancellation""
    }
}";
            return message;
        }

        public static string CreateTradeDetailsMessage(
            TradeEventType eventType,
            OrderSide orderSide,
            OrderType orderType,
            OrderStatus orderStatus,
            DateTimeOffset tradeAndCreateTime)
        {
            var message = @"{
    ""ch"": ""trade.clearing#btcusdt#0"",
    ""data"": {
         ""eventType"": """ + eventType.ToMessageValue() + @""",
         ""symbol"": ""btcusdt"",
         ""orderId"": 99998888,
         ""tradePrice"": ""9999.99"",
         ""tradeVolume"": ""0.96"",
         ""orderSide"": """ + orderSide.ToMessageValue() + @""",
         ""orderType"": """ + orderType.ToMessageValue() + @""",
         ""aggressor"": true,
         ""tradeId"": 919219323232,
         ""tradeTime"": " + tradeAndCreateTime.ToUnixTimeMilliseconds() + @",
         ""transactFee"": ""19.88"",
         ""feeDeduct "": ""0"",
         ""feeDeductType"": """",
         ""feeCurrency"": ""btc"",
         ""accountId"": 9912791,
         ""source"": ""spot-api"",
         ""orderPrice"": ""10000"",
         ""orderSize"": ""1"",
         ""clientOrderId"": ""a001"",
         ""orderCreateTime"": " + tradeAndCreateTime.ToUnixTimeMilliseconds() + @",
         ""orderStatus"": """ + orderStatus.ToMessageValue() + @"""
    }
}";
            return message;
        }

        public static string CreateAccountUpdateAccountBalanceChangedMessage(
            AccountChangeType changeType,
            AccountType accountType,
            DateTimeOffset changeTime)
        {
            var message = @"{
    ""action"": ""push"",
    ""ch"": ""accounts.update#0"",
    ""data"": {
        ""currency"": ""btc"",
        ""accountId"": 123456,
        ""balance"": ""23.111"",
        ""changeType"": """ + changeType.ToMessageValue() + @""",
        ""accountType"":""" + accountType.ToMessageValue() + @""",
        ""changeTime"": " + changeTime.ToUnixTimeMilliseconds() + @"
    }
}";
            return message;
        }

        public static string CreateAccountUpdateAvailableBalanceChangedMessage(
            AccountChangeType changeType,
            AccountType accountType,
            DateTimeOffset changeTime)
        {
            var message = @"{
    ""action"": ""push"",
    ""ch"": ""accounts.update#1"",
    ""data"": {
        ""currency"": ""btc"",
        ""accountId"": 123456,
        ""available"": ""23.111"",
        ""changeType"": """ + changeType.ToMessageValue() + @""",
        ""accountType"":""" + accountType.ToMessageValue() + @""",
        ""changeTime"": " + changeTime.ToUnixTimeMilliseconds() + @"
    }
}";
            return message;
        }
    }
}