using System;
using System.Collections;
using System.Collections.Generic;
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
                    Assert.True(msg.Data.ErrorCode > 0);
                });

            var message = HuobiAccountMessagesFactory.CreateConditionalOrderTriggeringFailureMessage(orderSide);

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
                    Assert.True(msg.Data.OrderTriggerTime > 0);
                });

            var message = HuobiAccountMessagesFactory.CreateConditionalOrderCanceledMessage(orderSide);

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
                    Assert.True(msg.Data.OrderId > 0);
                });

            var message = HuobiAccountMessagesFactory.CreateOrderSubmittedMessage(orderType);

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
                    Assert.True(msg.Data.TradeId > 0);
                    Assert.True(msg.Data.OrderId > 0);
                });

            var message = HuobiAccountMessagesFactory.CreateOrderTradedMessage(orderStatus, orderType);

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

        [Theory]
        [ClassData(typeof(TradeDetailsEnumsTestData))]
        public void TradeDetailsMessage_StreamUpdated(
            TradeEventType eventType,
            OrderSide orderSide,
            OrderType orderType,
            OrderStatus orderStatus)
        {
            // Arrange
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
                    Assert.True(msg.Data.OrderId > 0);
                });

            var message = HuobiAccountMessagesFactory.CreateTradeDetailsMessage(eventType, orderSide, orderType, orderStatus);

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
            foreach (var value in Enum.GetValues(typeof(OrderType)))
            {
                yield return new[]
                {
                    value!
                };
            }
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
            foreach (OrderType? orderType in Enum.GetValues(typeof(OrderType)))
            {
                foreach (OrderStatus? orderStatus in Enum.GetValues(typeof(OrderStatus)))
                {
                    yield return new[]
                    {
                        (object)orderType!,
                        orderStatus!
                    };
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TradeDetailsEnumsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (TradeEventType? eventType in Enum.GetValues(typeof(TradeEventType)))
            {
                foreach (OrderSide? orderSide in Enum.GetValues(typeof(OrderSide)))
                {
                    foreach (OrderType? orderType in Enum.GetValues(typeof(OrderType)))
                    {
                        foreach (OrderStatus? orderStatus in Enum.GetValues(typeof(OrderStatus)))
                        {
                            yield return new[]
                            {
                                (object)eventType!,
                                orderSide!,
                                orderType!,
                                orderStatus!
                            };
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}