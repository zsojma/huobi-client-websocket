using System;
using Huobi.Client.Websocket.Messages.Account;
using Moq;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class GenericMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void Ping_RespondsWithPong()
        {
            // Arrange
            Initialize();
            var message = new AuthPingRequest(12345);

            // Act
            TriggerMessageReceive(message);

            // Assert
            CommunicatorMock.Verify(m => m.Send(It.Is<string>(x => x.Contains("pong") && x.Contains("12345"))), Times.Once);
            VerifyMessageNotUnhandled();
        }

        [Fact]
        public void HandleResponse_AuthErrorMessage_StreamUpdated()
        {
            var triggered = false;
            var client = Initialize();
            client.Streams.AuthErrorMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                });

            var message = HuobiAuthMessagesFactory.CreateAuthErrorMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void HandleResponse_Authenticated_StreamUpdated()
        {
            var triggered = false;
            var client = Initialize();
            client.Streams.AuthenticationResponseStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                });

            var message = HuobiAuthMessagesFactory.CreateAuthenticationResponseMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}