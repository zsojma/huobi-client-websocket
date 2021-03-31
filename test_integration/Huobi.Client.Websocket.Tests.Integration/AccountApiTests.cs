using System;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.Account.Factories;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
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

        [Theory(Skip = "Requires setup of API keys")]
        [InlineData("", "")]
        public async Task AuthenticationRequest_ResponseMessageReceived(string accessKey, string secretKey)
        {
            // Arrange
            const string? url = HuobiConstants.ApiAuthWebsocketUrl;
            using var communicator = new HuobiWebsocketCommunicator(new Uri(url));

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            using var client = new HuobiWebsocketClient(
                communicator,
                serializer,
                NullLogger<HuobiWebsocketClient>.Instance);

            var dateTimeProvider = new HuobiDateTimeProvider();
            var authentication = new HuobiAuthentication();
            var config = Options.Create(
                new HuobiWebsocketClientConfig
                {
                    AccessKey = accessKey,
                    SecretKey = secretKey,
                    Url = url
                });
            var authRequestFactory = new HuobiAuthenticationRequestFactory(dateTimeProvider, authentication, config);

            var request = authRequestFactory.CreateRequest();

            AuthenticationResponse? response = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.AuthenticationResponseStream.Subscribe(
                message =>
                {
                    response = message;
                    receivedEvent.Set();
                });

            await communicator.Start();

            // Act
            client.Send(request);
            receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

            // Assert
            Assert.NotNull(response);
        }
    }
}