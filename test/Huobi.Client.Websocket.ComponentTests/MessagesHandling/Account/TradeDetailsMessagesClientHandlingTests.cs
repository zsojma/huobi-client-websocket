using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Huobi.Client.Websocket.Tests;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class TradeDetailsMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Theory]
        [ClassData(typeof(TradeDetailsEnumsTestData))]
        public void TradeDetailsMessage_StreamUpdated(
            string eventType,
            string orderSide,
            string orderType,
            string orderStatus)
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
                    Assert.True(EnumTestDataBase.EqualsWithString(eventType, msg.Data!.EventType));
                    Assert.True(EnumTestDataBase.EqualsWithString(orderSide, msg.Data!.OrderSide));
                    Assert.True(EnumTestDataBase.EqualsWithString(orderType, msg.Data!.OrderType));
                    Assert.True(EnumTestDataBase.EqualsWithString(orderStatus, msg.Data!.OrderStatus));
                    Assert.True(TestUtils.UnixTimesEqual(tradeAndCreateTime, msg.Data!.TradeTime));
                    Assert.True(TestUtils.UnixTimesEqual(tradeAndCreateTime, msg.Data!.OrderCreateTime));
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
                from string eventType in new TradeEventTypeTestData().GetValues()
                from string orderSide in new OrderSideTestData().GetValues()
                from string orderType in new OrderTypeTestData().GetValues()
                from string orderStatus in new OrderStatusTestData().GetValues()
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