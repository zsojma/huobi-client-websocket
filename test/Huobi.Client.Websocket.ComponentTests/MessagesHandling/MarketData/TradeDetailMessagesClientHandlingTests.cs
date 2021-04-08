using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.MarketData
{
    public class TradeDetailMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_StreamUpdated()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeMarketClient();
            client.Streams.TradeDetailUpdateStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketTradeDetail.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(msg.Tick.Id > 0);
                    Assert.NotNull(msg.Tick.Data);
                    Assert.Equal(2, msg.Tick.Data.Length);
                    Assert.True(msg.Tick.Data[0].TradeId > 0);
                    Assert.True(msg.Tick.Data[1].TradeId > 0);

                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Tick.Timestamp));
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Tick.Data[0].Timestamp));
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Tick.Data[1].Timestamp));
                });

            var message = HuobiMessagesFactory.CreateMarketTradeDetailUpdateMessage(timestamp);

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
            client.Streams.TradeDetailPullStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketTradeDetail.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.Equal(2, msg.Data.Length);
                    Assert.True(msg.Data[0].TradeId > 0);
                    Assert.True(msg.Data[1].TradeId > 0);

                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Timestamp));
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Data[0].Timestamp));
                    Assert.True(TestUtils.UnixTimesEqual(timestamp, msg.Data[1].Timestamp));
                });

            var message = HuobiMessagesFactory.CreateMarketTradeDetailPullResponseMessage(timestamp);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}