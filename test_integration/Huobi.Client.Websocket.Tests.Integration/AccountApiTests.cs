using System;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.Account;
using Xunit;

namespace Huobi.Client.Websocket.Tests.Integration
{
    public class AccountApiTests
    {
        [Fact]
        public async Task ConnectionEstablished_PingMessageReceived()
        {
            // Arrange
            using var client = HuobiWebsocketClientsFactory.CreateAccountClient(
                HuobiConstants.ApiAuthWebsocketUrl,
                "not_required_for_ping",
                "not_required_for_ping");

            AccountPingRequest? receivedPingMessage = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.PingAuthMessageStream.Subscribe(
                message =>
                {
                    receivedPingMessage = message;
                    receivedEvent.Set();
                });

            // Act
            await client.Start();

            // Assert
            receivedEvent.WaitOne(TimeSpan.FromSeconds(30));
            Assert.NotNull(receivedPingMessage);
        }

        [Theory(Skip = "Requires setup of API keys")]
        [InlineData("", "")]
        public async Task AuthenticationRequest_ResponseMessageReceived(string accessKey, string secretKey)
        {
            // Arrange
            using var client = HuobiWebsocketClientsFactory.CreateAccountClient(
                HuobiConstants.ApiAuthWebsocketUrl,
                accessKey,
                secretKey);

            AuthenticationResponse? response = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.AuthenticationResponseStream.Subscribe(
                message =>
                {
                    response = message;
                    receivedEvent.Set();
                });

            // Act
            await client.Start();

            // Assert
            receivedEvent.WaitOne(TimeSpan.FromSeconds(30));
            Assert.NotNull(response);
        }
    }
}