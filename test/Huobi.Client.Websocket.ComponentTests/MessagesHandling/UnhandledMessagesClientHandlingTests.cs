using Moq;
using Websocket.Client;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class UnhandledMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
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
    }
}