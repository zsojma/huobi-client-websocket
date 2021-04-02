using System;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.Pulling.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Xunit;

namespace Huobi.Client.Websocket.Tests.Integration
{
    public class MarketDataApiTests
    {
        [Fact]
        public async Task ConnectionEstablished_PingMessageReceived()
        {
            // Arrange
            using var client = HuobiWebsocketClientsFactory.CreateMarketClient(HuobiConstants.ApiWebsocketUrl);

            PingRequest? receivedPingMessage = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.PingMessageStream.Subscribe(
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

        [Fact]
        public async Task PullRequest_ResponseMessageReceived()
        {
            // Arrange
            using var client = HuobiWebsocketClientsFactory.CreateMarketClient(HuobiConstants.ApiWebsocketUrl);

            var request = new MarketCandlestickPullRequest("btcusdt", MarketCandlestickPeriodType.SixtyMinutes, "req1");

            MarketCandlestickPullResponse? response = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.CandlestickPullStream.Subscribe(
                message =>
                {
                    response = message;
                    receivedEvent.Set();
                });

            await client.Start();

            // Act
            client.Send(request);

            // Assert
            receivedEvent.WaitOne(TimeSpan.FromSeconds(30));
            Assert.NotNull(response);
        }
    }
}