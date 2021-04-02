using System.Collections;
using System.Collections.Generic;
using Moq;
using Websocket.Client;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class UnhandledMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Theory]
        [ClassData(typeof(UnknownMessageTestData))]
        public void MarketClient_UnknownContent_UnhandledStreamUpdated(string messageContent)
        {
            // Arrange
            InitializeMarketClient();

            // Act
            var compressed = Compress(messageContent);
            var binary = ResponseMessage.BinaryMessage(compressed);
            ResponseMessageSubject.OnNext(binary);

            // Assert
            UnhandledMessageObserverMock.Verify(m => m.OnNext(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(UnknownMessageTestData))]
        public void AccountClient_UnknownContent_UnhandledStreamUpdated(string messageContent)
        {
            // Arrange
            InitializeAccountClient();

            // Act
            var compressed = Compress(messageContent);
            var binary = ResponseMessage.BinaryMessage(compressed);
            ResponseMessageSubject.OnNext(binary);

            // Assert
            UnhandledMessageObserverMock.Verify(m => m.OnNext(It.IsAny<string>()), Times.Once);
        }

        internal class UnknownMessageTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "" };
                yield return new object[] { "{}"};
                yield return new object[] { "{ \"unknown\": \"value\" }"};
                yield return new object[] { "{ \"ping\": [not [parsable] }"};
                yield return new object[] { "{ \"action\": \"ping\", data: { [not [parsable] } }"};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}