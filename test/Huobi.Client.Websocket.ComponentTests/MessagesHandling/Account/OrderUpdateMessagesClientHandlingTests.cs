using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Tests;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class OrderUpdateMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Theory]
        [ClassData(typeof(OrderSideTestData))]
        public void ConditionalOrderTriggeringFailureMessage_StreamUpdated(string orderSide)
        {
            // Arrange
            var lastActTime = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.ConditionalOrderTriggerFailureMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(OrderEventType.Trigger, msg.Data!.EventType);
                    Assert.True(EnumTestDataBase.EqualsWithString(orderSide, msg.Data!.OrderSide));
                    Assert.Equal(OrderStatus.Rejected, msg.Data!.OrderStatus);
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data!.OrderTriggeringFailureTime));
                });

            var message = HuobiAccountMessagesFactory.CreateConditionalOrderTriggeringFailureMessage(orderSide, lastActTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Theory]
        [ClassData(typeof(OrderSideTestData))]
        public void ConditionalOrderCanceledMessage_StreamUpdated(string orderSide)
        {
            // Arrange
            var lastActTime = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.ConditionalOrderCanceledMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(OrderEventType.Deletion, msg.Data!.EventType);
                    Assert.True(EnumTestDataBase.EqualsWithString(orderSide, msg.Data!.OrderSide));
                    Assert.Equal(OrderStatus.Canceled, msg.Data!.OrderStatus);
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data!.OrderTriggerTime));
                });

            var message = HuobiAccountMessagesFactory.CreateConditionalOrderCanceledMessage(orderSide, lastActTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Theory]
        [ClassData(typeof(OrderTypeTestData))]
        public void OrderSubmittedMessage_StreamUpdated(string orderType)
        {
            // Arrange
            var lastActTime = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.OrderSubmittedMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(OrderEventType.Creation, msg.Data!.EventType);
                    Assert.Equal(OrderStatus.Submitted, msg.Data!.OrderStatus);
                    Assert.True(EnumTestDataBase.EqualsWithString(orderType, msg.Data!.Type));
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data!.OrderCreateTime));
                });

            var message = HuobiAccountMessagesFactory.CreateOrderSubmittedMessage(orderType, lastActTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Theory]
        [ClassData(typeof(OrderTradedEnumsTestData))]
        public void OrderTradedMessage_StreamUpdated(string orderType, string orderStatus)
        {
            // Arrange
            var lastActTime = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.OrderTradedMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(OrderEventType.Trade, msg.Data!.EventType);
                    Assert.True(EnumTestDataBase.EqualsWithString(orderStatus, msg.Data!.OrderStatus));
                    Assert.True(EnumTestDataBase.EqualsWithString(orderType, msg.Data!.Type));
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data!.TradeTime));
                });

            var message = HuobiAccountMessagesFactory.CreateOrderTradedMessage(orderStatus, orderType, lastActTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Theory]
        [ClassData(typeof(OrderTypeTestData))]
        public void OrderCanceledMessage_StreamUpdated(string orderType)
        {
            // Arrange
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.OrderCanceledMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(OrderEventType.Cancellation, msg.Data!.EventType);
                    Assert.Equal(OrderStatus.Canceled, msg.Data!.OrderStatus);
                    Assert.True(EnumTestDataBase.EqualsWithString(orderType, msg.Data!.Type));
                    Assert.True(msg.Data!.OrderId > 0);
                });

            var message = HuobiAccountMessagesFactory.CreateOrderCanceledMessage(orderType);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
    
    public class OrderTradedEnumsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var query =
                from string orderType in new OrderTypeTestData().GetValues()
                from string orderStatus in new OrderStatusTestData().GetValues()
                select new[]
                {
                    (object)orderType,
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