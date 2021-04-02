﻿using System;
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
                });

            var message = HuobiMessagesFactory.CreateMarketTradeDetailUpdateMessage();

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
                });

            var message = HuobiMessagesFactory.CreateMarketTradeDetailPullResponseMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}