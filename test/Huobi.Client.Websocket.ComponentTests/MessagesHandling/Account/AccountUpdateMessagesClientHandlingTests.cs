using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Huobi.Client.Websocket.Tests;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class AccountUpdateMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Theory]
        [ClassData(typeof(AccountUpdateEnumsTestData))]
        public void AccountUpdateMessage_AccountBalanceChanged_StreamUpdated(
            string changeType,
            string accountType)
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
                    Assert.True(EnumTestDataBase.EqualsWithString(changeType, msg.Data!.ChangeType));
                    Assert.True(EnumTestDataBase.EqualsWithString(accountType, msg.Data!.AccountType));
                    Assert.True(TestUtils.UnixTimesEqual(changeTime, msg.Data!.ChangeTime));
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
            string changeType,
            string accountType)
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
                    Assert.True(EnumTestDataBase.EqualsWithString(changeType, msg.Data!.ChangeType));
                    Assert.True(EnumTestDataBase.EqualsWithString(accountType, msg.Data!.AccountType));
                    Assert.True(TestUtils.UnixTimesEqual(changeTime, msg.Data!.ChangeTime));
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
                from string changeType in new AccountChangeTypeTestData().GetValues()
                from string accountType in new AccountTypeTestData().GetValues()
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