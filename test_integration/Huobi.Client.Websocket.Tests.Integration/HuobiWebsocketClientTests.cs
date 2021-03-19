using System;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages;
using Huobi.Client.Websocket.Messages.Pulling.MarketCandlestick;
using Huobi.Client.Websocket.Messages.Values;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Huobi.Client.Websocket.Tests.Integration
{
    public class HuobiWebsocketClientTests
    {
        [Fact]
        public async Task ConnectionEstablished_PingMessageReceived()
        {
            // Arrange
            const string? url = HuobiConstants.ApiWebsocketUrl;
            using var communicator = new HuobiWebsocketCommunicator(new Uri(url));

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            using var client = new HuobiWebsocketClient(
                communicator,
                serializer,
                NullLogger<HuobiWebsocketClient>.Instance);

            PingMessage? receivedPingMessage = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.PingMessageStream.Subscribe(message =>
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

        [Fact]
        public async Task PullRequest_ResponseMessageReceived()
        {
            // Arrange
            const string? url = HuobiConstants.ApiWebsocketUrl;
            using var communicator = new HuobiWebsocketCommunicator(new Uri(url));

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            using var client = new HuobiWebsocketClient(
                communicator,
                serializer,
                NullLogger<HuobiWebsocketClient>.Instance);

            var request = new MarketCandlestickPullRequest("btcusdt", MarketCandlestickPeriodType.SixtyMinutes, "req1");

            MarketCandlestickPullResponse? response = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.MarketCandlestickPullStream.Subscribe(message =>
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