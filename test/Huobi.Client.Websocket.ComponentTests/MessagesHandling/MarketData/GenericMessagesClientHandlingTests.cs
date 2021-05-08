using System;
using Huobi.Client.Websocket.Messages.MarketData;
using Moq;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.MarketData
{
    public class GenericMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void Ping_RespondsWithPong()
        {
            // Arrange
            InitializeMarketClient();
            var message = new PingRequest(12345);

            // Act
            TriggerMessageReceive(message);

            // Assert
            CommunicatorMock.Verify(m => m.Send(It.Is<string>(x => x.Contains("pong") && x.Contains("12345"))), Times.Once);
            VerifyMessageNotUnhandled();
        }

        [Fact]
        public void HandleResponse_ErrorMessage_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeMarketClient();
            client.Streams.ErrorMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                });

            var message = HuobiMessagesFactory.CreateErrorMessage(timestamp);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void HandleResponse_Subscribed_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeMarketClient();
            client.Streams.SubscribeResponseStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(!string.IsNullOrEmpty(msg.Status));
                    Assert.True(!string.IsNullOrEmpty(msg.ReqId));
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                });

            var message = HuobiMessagesFactory.CreateSubscribeResponseMessage(timestamp);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void HandleResponse_Unsubscribed_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeMarketClient();
            client.Streams.UnsubscribeResponseStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(!string.IsNullOrEmpty(msg.Status));
                    Assert.True(!string.IsNullOrEmpty(msg.ReqId));
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                });

            var message = HuobiMessagesFactory.CreateUnsubscribeResponseMessage(timestamp);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}