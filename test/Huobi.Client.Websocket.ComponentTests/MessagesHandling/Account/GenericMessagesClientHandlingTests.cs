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
    }
}