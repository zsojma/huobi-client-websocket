using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.MarketData
{
    public class CandlestickMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeMarketClient();
            client.Streams.CandlestickUpdateStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketCandlestick.ToTopicId(), msg.Topic);
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                    Assert.True(msg.Tick!.Id > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketCandlestickUpdateMessage(timestamp);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void PullMessage_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeMarketClient();
            client.Streams.CandlestickPullStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Contains(SubscriptionType.MarketCandlestick.ToTopicId(), msg.Topic);
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                    Assert.Equal(2, msg.Data!.Length);
                    Assert.True(msg.Data[0].Id > 0);
                    Assert.True(msg.Data[1].Id > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketCandlestickPullResponseMessage(timestamp);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}