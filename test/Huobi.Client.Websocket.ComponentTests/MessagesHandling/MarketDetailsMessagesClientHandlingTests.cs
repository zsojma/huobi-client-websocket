using System;
using Huobi.Client.Websocket.Messages.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class MarketDetailsMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_StreamUpdated()
        {
            // Arrange
            var triggered = false;
            var client = Initialize();
            client.Streams.MarketDetailsUpdateStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketDetails.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(msg.Tick.Id > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketDetailsUpdateMessage();

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
            var triggered = false;
            var client = Initialize();
            client.Streams.MarketDetailsPullStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketDetails.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(msg.Data.Id > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketDetailsPullResponseMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}