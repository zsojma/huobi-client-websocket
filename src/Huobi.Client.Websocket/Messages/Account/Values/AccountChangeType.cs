namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public enum AccountChangeType
    {
        Unknown,
        OrderPlace,
        OrderMatch,
        OrderRefund,
        OrderCancel,
        OrderFeeRefund,
        MarginTransfer,
        MarginLoan,
        MarginInterest,
        MarginRepay,
        Deposit,
        Withdraw,
        Other
    }
}