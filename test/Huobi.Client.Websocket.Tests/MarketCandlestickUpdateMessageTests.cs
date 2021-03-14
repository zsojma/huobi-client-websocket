using Huobi.Client.Websocket.Messages.Subscription.MarketCandlestick;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Huobi.Client.Websocket.Tests
{
    public class MarketCandlestickUpdateMessageTests
    {
        [Fact]
        public void TryParse_CorrectMessage_ParsedCorrectly()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var input = @"{
  ""ch"": ""market.ethbtc.kline.1min"",
  ""ts"": 1489474082831, //system update time
  ""tick"": {
    ""id"": 1489464480,
    ""amount"": 10.11,
    ""count"": 12,
    ""open"": 7962.62,
    ""close"": 7962.63,
    ""low"": 7962.64,
    ""high"": 7962.65,
    ""vol"": 102.30
  }
}";

            // Act
            var result = MarketCandlestickUpdateMessage.TryParse(serializer, input, out var response);

            // Assert
            Assert.True(result);
            Assert.Equal(1489464480, response!.Tick.Id);
            Assert.Equal(10.11m, response!.Tick.Amount);
            Assert.Equal(12, response!.Tick.Count);
            Assert.Equal(7962.62m, response!.Tick.Open);
            Assert.Equal(7962.63m, response!.Tick.Close);
            Assert.Equal(7962.64m, response!.Tick.Low);
            Assert.Equal(7962.65m, response!.Tick.High);
            Assert.Equal(102.30m, response!.Tick.Vol);
        }
        
        [Fact]
        public void TryParse_DiffMessageContainsKeywords_ReturnsFalse()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var input = @"{
  ""sub"": ""market.btcusdt.kline.1min"",
  ""id"": ""id1"",
  ""tick"": {}
}";

            // Act
            var result = MarketCandlestickUpdateMessage.TryParse(serializer, input, out _);

            // Assert
            Assert.False(result);
        }
    }
}