using System;
using Huobi.Client.Websocket.Messages.MarketData;
using Moq;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class GenericClientMessageHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void Ping_RespondsWithPong()
        {
            // Arrange
            InitializeGenericClient();
            var message = new PingRequest(12345);

            // Act
            TriggerMessageReceive(message);

            // Assert
            GenericCommunicatorMock.Verify(m => m.Send(It.Is<string>(x => x.Contains("pong") && x.Contains("12345"))), Times.Once);
            VerifyMessageNotUnhandled();
        }

        [Fact]
        public void HandleResponse_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var message = HuobiMessagesFactory.CreateSubscribeResponseMessage(timestamp);

            var triggered = false;
            var client = InitializeGenericClient();
            client.Streams.ResponseMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.Equal(message, msg);
                });

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}