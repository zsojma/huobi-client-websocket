using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.MarketData
{
    public class DepthMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeMarketClient();
            client.Streams.DepthUpdateStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketDepth.ToTopicId(), msg.Topic);
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                    Assert.Equal(2, msg.Tick.Bids.Length);
                    Assert.True(msg.Tick.Bids[0].Price > 0);
                    Assert.Equal(2, msg.Tick.Asks.Length);
                    Assert.True(msg.Tick.Asks[0].Price > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketDepthUpdateMessage(timestamp);

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
            client.Streams.DepthPullStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketDepth.ToTopicId(), msg.Topic);
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                    Assert.Equal(2, msg.Data.Bids.Length);
                    Assert.True(msg.Data.Bids[0].Price > 0);
                    Assert.Equal(2, msg.Data.Asks.Length);
                    Assert.True(msg.Data.Asks[0].Price > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketDepthPullResponseMessage(timestamp);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}