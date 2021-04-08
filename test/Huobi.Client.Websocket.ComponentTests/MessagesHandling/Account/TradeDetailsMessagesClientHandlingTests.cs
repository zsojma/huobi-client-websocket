using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Huobi.Client.Websocket.Messages.Account.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class TradeDetailsMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Theory]
        [ClassData(typeof(TradeDetailsEnumsTestData))]
        public void TradeDetailsMessage_StreamUpdated(
            TradeEventType eventType,
            OrderSide orderSide,
            OrderType orderType,
            OrderStatus orderStatus)
        {
            // Arrange
            var tradeAndCreateTime = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.TradeDetailsMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(eventType, msg.Data.EventType);
                    Assert.Equal(orderSide, msg.Data.OrderSide);
                    Assert.Equal(orderType, msg.Data.OrderType);
                    Assert.Equal(orderStatus, msg.Data.OrderStatus);
                    Assert.True(TestUtils.UnixTimesEqual(tradeAndCreateTime, msg.Data.TradeTime));
                    Assert.True(TestUtils.UnixTimesEqual(tradeAndCreateTime, msg.Data.OrderCreateTime));
                });

            var message = HuobiAccountMessagesFactory.CreateTradeDetailsMessage(
                eventType,
                orderSide,
                orderType,
                orderStatus,
                tradeAndCreateTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }

    public class TradeDetailsEnumsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var query =
                from TradeEventType eventType in Enum.GetValues(typeof(TradeEventType))
                from OrderSide orderSide in Enum.GetValues(typeof(OrderSide))
                from OrderType orderType in Enum.GetValues(typeof(OrderType))
                from OrderStatus orderStatus in Enum.GetValues(typeof(OrderStatus))
                select new[]
                {
                    (object)eventType,
                    orderSide,
                    orderType,
                    orderStatus
                };
            return query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}