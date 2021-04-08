using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Huobi.Client.Websocket.Messages.Account.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class OrderUpdateMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Theory]
        [InlineData(OrderSide.Buy)]
        [InlineData(OrderSide.Sell)]
        public void ConditionalOrderTriggeringFailureMessage_StreamUpdated(OrderSide orderSide)
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
                    Assert.Equal(OrderEventType.Trigger, msg.Data.EventType);
                    Assert.Equal(orderSide, msg.Data.OrderSide);
                    Assert.Equal(OrderStatus.Rejected, msg.Data.OrderStatus);
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data.OrderTriggeringFailureTime));
                });

            var message = HuobiAccountMessagesFactory.CreateConditionalOrderTriggeringFailureMessage(orderSide, lastActTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Theory]
        [InlineData(OrderSide.Buy)]
        [InlineData(OrderSide.Sell)]
        public void ConditionalOrderCanceledMessage_StreamUpdated(OrderSide orderSide)
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
                    Assert.Equal(OrderEventType.Deletion, msg.Data.EventType);
                    Assert.Equal(orderSide, msg.Data.OrderSide);
                    Assert.Equal(OrderStatus.Canceled, msg.Data.OrderStatus);
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data.OrderTriggerTime));
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
        public void OrderSubmittedMessage_StreamUpdated(OrderType orderType)
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
                    Assert.Equal(OrderEventType.Creation, msg.Data.EventType);
                    Assert.Equal(OrderStatus.Submitted, msg.Data.OrderStatus);
                    Assert.Equal(orderType, msg.Data.OrderType);
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data.OrderCreateTime));
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
        public void OrderTradedMessage_StreamUpdated(OrderStatus orderStatus, OrderType orderType)
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
                    Assert.Equal(OrderEventType.Trade, msg.Data.EventType);
                    Assert.Equal(orderStatus, msg.Data.OrderStatus);
                    Assert.Equal(orderType, msg.Data.OrderType);
                    Assert.True(TestUtils.UnixTimesEqual(lastActTime, msg.Data.TradeTime));
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
        public void OrderCanceledMessage_StreamUpdated(OrderType orderType)
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
                    Assert.Equal(OrderEventType.Cancellation, msg.Data.EventType);
                    Assert.Equal(OrderStatus.Canceled, msg.Data.OrderStatus);
                    Assert.Equal(orderType, msg.Data.OrderType);
                    Assert.True(msg.Data.OrderId > 0);
                });

            var message = HuobiAccountMessagesFactory.CreateOrderCanceledMessage(orderType);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }

    public class OrderTypeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var query =
                from OrderType value in Enum.GetValues(typeof(OrderType))
                select new[]
                {
                    (object)value
                };
            return query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class OrderTradedEnumsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var query =
                from OrderType orderType in Enum.GetValues(typeof(OrderType))
                from OrderStatus orderStatus in Enum.GetValues(typeof(OrderStatus))
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