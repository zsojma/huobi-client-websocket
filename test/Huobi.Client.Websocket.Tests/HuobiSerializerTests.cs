using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Huobi.Client.Websocket.Tests
{
    public class HuobiSerializerTests
    {
        [Fact]
        public void Serialize_SimpleObject_ReturnsCorrectValue()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.Serialize(
                new
                {
                    value = 11.2
                });

            // Assert
            Assert.NotNull(result);
            Assert.Contains("value", result);
            Assert.Contains("11.2", result);
        }

        [Fact]
        public void TryDeserializeIfContains_SingleContains_MessageParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", "ping", out var message);

            // Assert
            Assert.True(result);
            Assert.NotNull(message);
            Assert.Equal(1234, message!.Value);
        }

        [Fact]
        public void TryDeserializeIfContains_SingleContains_MessageNotParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", "unknown", out var message);

            // Assert
            Assert.False(result);
            Assert.Null(message);
        }

        [Fact]
        public void TryDeserializeIfContains_MultiContains_MessageParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", new [] { "\"ping\"", "23" }, out var message);

            // Assert
            Assert.True(result);
            Assert.NotNull(message);
            Assert.Equal(1234, message!.Value);
        }

        [Fact]
        public void TryDeserializeIfContains_MultiContains_MessageNotParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", new [] { "\"ping\"", "unknown" }, out var message);

            // Assert
            Assert.False(result);
            Assert.Null(message);
        }

        [Fact]
        public void TryDeserializeIfContains_SingleNotContains_MessageParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", "\"ping\"", "\"unknown\"", out var message);

            // Assert
            Assert.True(result);
            Assert.NotNull(message);
            Assert.Equal(1234, message!.Value);
        }

        [Fact]
        public void TryDeserializeIfContains_SingleNotContains_MessageNotParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", "\"ping\"", "34", out var message);

            // Assert
            Assert.False(result);
            Assert.Null(message);
        }

        [Fact]
        public void TryDeserializeIfContains_MultiNotContains_MessageParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", new [] { "\"ping\"", "23" }, new [] { "\"unknown\"", "456" }, out var message);

            // Assert
            Assert.True(result);
            Assert.NotNull(message);
            Assert.Equal(1234, message!.Value);
        }

        [Fact]
        public void TryDeserializeIfContains_MultiNotContains_MessageNotParsed()
        {
            // Arrange
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            // Act
            var result = serializer.TryDeserializeIfContains<PingRequest>("{ \"ping\": 1234 }", new [] { "\"ping\"", "23" }, new [] { "34", "\"unknown\"" }, out var message);

            // Assert
            Assert.False(result);
            Assert.Null(message);
        }
    }
}