using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Huobi.Client.Websocket.Messages.Account.Values;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class AccountUpdateMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Theory]
        [ClassData(typeof(AccountUpdateEnumsTestData))]
        public void AccountUpdateMessage_AccountBalanceChanged_StreamUpdated(
            AccountChangeType changeType,
            AccountType accountType)
        {
            // Arrange
            var changeTime = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.AccountUpdateMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(changeType, msg.Data.ChangeType);
                    Assert.Equal(accountType, msg.Data.AccountType);
                    Assert.True(TestUtils.UnixTimesEqual(changeTime, msg.Data.ChangeTime!.Value));
                });

            var message = HuobiAccountMessagesFactory.CreateAccountUpdateAccountBalanceChangedMessage(
                changeType,
                accountType,
                changeTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Theory]
        [ClassData(typeof(AccountUpdateEnumsTestData))]
        public void AccountUpdateMessage_AvailableBalanceChanged_StreamUpdated(
            AccountChangeType changeType,
            AccountType accountType)
        {
            // Arrange
            var changeTime = DateTimeOffset.UtcNow;
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.AccountUpdateMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.NotNull(msg.Data);
                    Assert.Equal(changeType, msg.Data.ChangeType);
                    Assert.Equal(accountType, msg.Data.AccountType);
                    Assert.True(TestUtils.UnixTimesEqual(changeTime, msg.Data.ChangeTime!.Value));
                });

            var message = HuobiAccountMessagesFactory.CreateAccountUpdateAvailableBalanceChangedMessage(
                changeType,
                accountType,
                changeTime);

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }

    public class AccountUpdateEnumsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var query =
                from AccountChangeType changeType in Enum.GetValues(typeof(AccountChangeType))
                from AccountType accountType in Enum.GetValues(typeof(AccountType))
                select new[]
                {
                    (object)changeType,
                    accountType
                };
            return query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}