using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class AccountChangeTypeHelper
    {
        public static AccountChangeType? FromNullableMessageValue(string? accountChangeType)
        {
            return accountChangeType switch
            {
                "order-place" => AccountChangeType.OrderPlace,
                "order-match" => AccountChangeType.OrderMatch,
                "order-refund" => AccountChangeType.OrderRefund,
                "order-cancel" => AccountChangeType.OrderCancel,
                "order-fee-refund" => AccountChangeType.OrderFeeRefund,
                "margin-transfer" => AccountChangeType.MarginTransfer,
                "margin-loan" => AccountChangeType.MarginLoan,
                "margin-interest" => AccountChangeType.MarginInterest,
                "margin-repay" => AccountChangeType.MarginRepay,
                _ => Enum.TryParse(accountChangeType, true, out AccountChangeType value)
                    ? value
                    : null
            };
        }

        public static AccountChangeType FromMessageValue(string accountChangeType)
        {
            return FromNullableMessageValue(accountChangeType)
                ?? throw new ArgumentOutOfRangeException(
                       nameof(accountChangeType),
                       accountChangeType,
                       $"Unable to translate {accountChangeType} to account change type!");
        }

        public static string ToMessageValue(this AccountChangeType accountChangeType)
        {
            return accountChangeType switch
            {
                AccountChangeType.OrderPlace => "order-place",
                AccountChangeType.OrderMatch => "order-match",
                AccountChangeType.OrderRefund => "order-refund",
                AccountChangeType.OrderCancel => "order-cancel",
                AccountChangeType.OrderFeeRefund => "order-fee-refund",
                AccountChangeType.MarginTransfer => "margin-transfer",
                AccountChangeType.MarginInterest => "margin-interest",
                AccountChangeType.MarginRepay => "margin-repay",
                _ => accountChangeType.ToString().ToLower()
            };
        }
    }
}