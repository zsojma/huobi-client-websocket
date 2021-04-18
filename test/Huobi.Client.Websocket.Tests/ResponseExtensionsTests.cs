using System;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;
using Xunit;

namespace Huobi.Client.Websocket.Tests
{
    public class ResponseExtensionsTests
    {
        [Theory]
        [InlineData("market.btcusdt.trade.detail", "btcusdt")]
        [InlineData("market.btcusdt.mbp.5", "btcusdt")]
        [InlineData("market.btcusdt.mbp.refresh.20", "btcusdt")]
        [InlineData("market.ethbtc.mbp.refresh.20", "ethbtc")]
        public void ParseSymbolFromTopic_ValidTopic_ReturnsCorrectData(string topic, string expectedSymbol)
        {
            // Arrange
            var updateMessage = new UpdateMessage<object>(topic, DateTimeOffset.UtcNow, new object());

            // Act
            var updateSymbol = updateMessage.ParseSymbolFromTopic();

            // Assert
            Assert.Equal(expectedSymbol, updateSymbol);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("market.")]
        [InlineData("not-valid")]
        public void ParseSymbolFromTopic_InvalidTopic_ThrowsException(string topic)
        {
            // Arrange
            var updateMessage = new MarketTradeDetailUpdateMessage(
                topic,
                DateTimeOffset.UtcNow,
                new MarketTradeDetailTick(1, DateTimeOffset.UtcNow, Array.Empty<MarketTradeDetailTickDataItem>()));

            // Act & Assert
            Assert.Throws<HuobiWebsocketClientException>(() => updateMessage.ParseSymbolFromTopic());
        }
    }
}