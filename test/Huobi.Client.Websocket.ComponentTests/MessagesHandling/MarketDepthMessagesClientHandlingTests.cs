using System;
using Huobi.Client.Websocket.Messages.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class MarketDepthMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_StreamUpdated()
        {
            // Arrange
            var client = Initialize();
            client.Streams.MarketDepthUpdateStream.Subscribe(
                msg =>
                {
                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketDepth.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(msg.Tick.Timestamp > 0);
                    Assert.Equal(2, msg.Tick.Bids.Length);
                    Assert.True(msg.Tick.Bids[0][0] > 0);
                    Assert.Equal(2, msg.Tick.Asks.Length);
                    Assert.True(msg.Tick.Asks[0][0] > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketDepthUpdateMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
        }

        [Fact]
        public void PullMessage_StreamUpdated()
        {
            // Arrange
            var client = Initialize();
            client.Streams.MarketDepthPullStream.Subscribe(
                msg =>
                {
                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketDepth.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(msg.Data.Timestamp > 0);
                    Assert.Equal(2, msg.Data.Bids.Length);
                    Assert.True(msg.Data.Bids[0][0] > 0);
                    Assert.Equal(2, msg.Data.Asks.Length);
                    Assert.True(msg.Data.Asks[0][0] > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketDepthPullMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
        }
    }
}