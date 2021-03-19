using System;
using Huobi.Client.Websocket.Messages;
using Moq;
using Websocket.Client;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class GenericMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void Ping_RespondsWithPong()
        {
            // Arrange
            Initialize();
            var message = new PingMessage(12345);

            // Act
            TriggerMessageReceive(message);

            // Assert
            CommunicatorMock.Verify(m => m.Send(It.Is<string>(x => x.Contains("pong") && x.Contains("12345"))), Times.Once);
            VerifyMessageNotUnhandled();
        }

        [Theory]
        [InlineData("")]
        [InlineData("{}")]
        [InlineData("{ \"unknown\": \"value\" }")]
        [InlineData("{ \"ping\": [not [parsable] }")]
        public void UnknownContent_UnhandledStreamUpdated(string messageContent)
        {
            // Arrange
            Initialize();

            // Act
            var compressed = Compress(messageContent);
            var binary = ResponseMessage.BinaryMessage(compressed);
            ResponseMessageSubject.OnNext(binary);

            // Assert
            UnhandledMessageObserverMock.Verify(m => m.OnNext(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void HandleResponse_Subscribed_StreamUpdated()
        {
            var client = Initialize();
            client.Streams.SubscribeResponseStream.Subscribe(
                msg =>
                {
                    // Assert
                    Assert.NotNull(msg);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(!string.IsNullOrEmpty(msg.Status));
                    Assert.True(!string.IsNullOrEmpty(msg.ReqId));
                    Assert.True(msg.Timestamp > 0);
                });

            var message = HuobiMessagesFactory.CreateSubscribeResponseMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
        }
    }
}