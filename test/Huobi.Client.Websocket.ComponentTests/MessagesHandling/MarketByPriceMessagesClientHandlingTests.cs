using System;
using Huobi.Client.Websocket.Messages.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class MarketByPriceMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_StreamUpdated()
        {
            // Arrange
            var client = Initialize();
            client.Streams.MarketCandlestickUpdateStream.Subscribe(
                msg =>
                {
                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketCandlestick.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(msg.Tick.Id > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketCandlestickUpdateMessage();

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
            client.Streams.MarketCandlestickPullStream.Subscribe(
                msg =>
                {
                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketCandlestick.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.Equal(2, msg.Data.Length);
                    Assert.True(msg.Data[0].Id > 0);
                    Assert.True(msg.Data[1].Id > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketCandlestickPullMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
        }
    }
}