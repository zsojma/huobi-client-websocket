using System;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Huobi.Client.Websocket.Tests.Integration
{
    public class AccountApiTests
    {
        [Fact]
        public async Task ConnectionEstablished_PingMessageReceived()
        {
            // Arrange
            const string? url = HuobiConstants.ApiAuthWebsocketUrl;
            using var communicator = new HuobiWebsocketCommunicator(new Uri(url));

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            using var client = new HuobiWebsocketClient(
                communicator,
                serializer,
                NullLogger<HuobiWebsocketClient>.Instance);

            AuthPingRequest? receivedPingMessage = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.PingAuthMessageStream.Subscribe(
                message =>
                {
                    receivedPingMessage = message;
                    receivedEvent.Set();
                });

            // Act
            await communicator.Start();
            receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

            // Assert
            Assert.NotNull(receivedPingMessage);
        }
    }
}