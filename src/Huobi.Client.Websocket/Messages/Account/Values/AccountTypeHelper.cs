using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class AccountTypeHelper
    {
        public static AccountType FromMessageValue(string accountType)
        {
            return Enum.TryParse(accountType, true, out AccountType value)
                ? value
                : throw new ArgumentOutOfRangeException(
                    nameof(accountType),
                    accountType,
                    $"Unable to translate {accountType} to account type!");
        }

        public static string ToMessageValue(this AccountType accountType)
        {
            return accountType.ToString().ToLower();
        }
    }
}