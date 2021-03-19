﻿using System;
using Huobi.Client.Websocket.Messages.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class MarketBestBidOfferMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void UpdateMessage_StreamUpdated()
        {
            // Arrange
            var client = Initialize();
            client.Streams.MarketBestBidOfferUpdateStream.Subscribe(
                msg =>
                {
                    // Assert
                    Assert.NotNull(msg);
                    Assert.Contains(SubscriptionType.MarketBestBidOffer.ToTopicId(), msg.Topic);
                    Assert.True(!string.IsNullOrEmpty(msg.Topic));
                    Assert.True(msg.Tick.SeqId > 0);
                    Assert.True(msg.Tick.Bid > 0);
                    Assert.True(msg.Tick.Ask > 0);
                });

            var message = HuobiMessagesFactory.CreateMarketBestBidOfferUpdateMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
        }
    }
}