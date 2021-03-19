using System;
using Huobi.Client.Websocket.Messages.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class MarketByPriceMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_Asks_StreamUpdated()
        {
            // Arrange
            var triggered = false;
            var client = Initialize();
            client.Streams.MarketByPriceUpdateStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketByPrice.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.NotNull(msg.Tick.Asks);
                    Assert.Single(msg.Tick.Asks!);
                    Assert.True(msg.Tick.Asks![0][0] > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketByPriceUpdateMessage_Asks();
            
            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void UpdateMessage_Bids_StreamUpdated()
        {
            // Arrange
            var triggered = false;
            var client = Initialize();
            client.Streams.MarketByPriceUpdateStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketByPrice.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.NotNull(msg.Tick.Bids);
                    Assert.Single(msg.Tick.Bids!);
                    Assert.True(msg.Tick.Bids![0][0] > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketByPriceUpdateMessage_Bids();
            
            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void UpdateMessage_Refresh_StreamUpdated()
        {
            // Arrange
            var triggered = false;
            var client = Initialize();
            client.Streams.MarketByPriceRefreshUpdateStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketByPriceRefresh.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.NotNull(msg.Tick.Bids);
                    Assert.Equal(5, msg.Tick.Bids!.Length);
                    Assert.True(msg.Tick.Bids[0][0] > 0);
                    Assert.NotNull(msg.Tick.Asks);
                    Assert.Equal(5, msg.Tick.Asks!.Length);
                    Assert.True(msg.Tick.Asks[0][0] > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketByPriceRefreshUpdateMessage();
            
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
            client.Streams.MarketByPricePullStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketByPrice.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.Equal(5, msg.Data.Bids.Length);
                    Assert.True(msg.Data.Bids[0][0] > 0);
                    Assert.Equal(5, msg.Data.Asks.Length);
                    Assert.True(msg.Data.Asks[0][0] > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketByPricePullResponseMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}