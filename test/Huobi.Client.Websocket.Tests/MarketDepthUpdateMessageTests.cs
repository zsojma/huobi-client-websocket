using Huobi.Client.Websocket.Messages.Subscription.Depth;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Huobi.Client.Websocket.Tests
{
    public class MarketDepthUpdateMessageTests
    {
        [Fact]
        public void TryParse_CorrectMessage_ParsedCorrectly()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var input = @"{
  ""ch"": ""market.htusdt.depth.step0"",
  ""ts"": 1572362902027,
  ""tick"": {
    ""bids"": [
      [3.7721, 344.86],
      [3.7709, 46.66]
    ],
    ""asks"": [
      [3.7745, 15.44],
      [3.7746, 70.52]
    ],
    ""version"": 100434317651,
    ""ts"": 1572362902012
  }
}";

            // Act
            var result = MarketDepthUpdateMessage.TryParse(serializer, input, out var response);

            // Assert
            Assert.True(result);
            Assert.Equal(3.7721m, response!.Tick.Bids[0][0]);
            Assert.Equal(344.86m, response!.Tick.Bids[0][1]);
            Assert.Equal(3.7709m, response!.Tick.Bids[1][0]);
            Assert.Equal(46.66m, response!.Tick.Bids[1][1]);
            Assert.Equal(3.7745m, response!.Tick.Asks[0][0]);
            Assert.Equal(15.44m, response!.Tick.Asks[0][1]);
            Assert.Equal(3.7746m, response!.Tick.Asks[1][0]);
            Assert.Equal(70.52m, response!.Tick.Asks[1][1]);
            Assert.Equal(100434317651, response!.Tick.Version);
            Assert.Equal(1572362902012, response!.Tick.Timestamp);
        }

        [Fact]
        public void TryParse_DiffMessageContainsKeywords_ReturnsFalse()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var input = @"{
  ""sub"": ""market.btcusdt.depth.step0"",
  ""id"": ""id1"",
  ""tick"": {}
}";

            // Act
            var result = MarketDepthUpdateMessage.TryParse(serializer, input, out _);

            // Assert
            Assert.False(result);
        }
    }
}