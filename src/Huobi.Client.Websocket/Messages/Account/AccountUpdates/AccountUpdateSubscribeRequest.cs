using Huobi.Client.Websocket.Messages.Account.Values;

namespace Huobi.Client.Websocket.Messages.Account.AccountUpdates;

public class AccountUpdateSubscribeRequest : AccountSubscribeRequest
{
    public AccountUpdateSubscribeRequest(bool withAvailableBalanceChanges = false)
        : base(FormatSymbol(withAvailableBalanceChanges), AccountSubscriptionType.AccountUpdates)
    {
    }

    private static string FormatSymbol(bool withAvailableBalanceChanges)
    {
        return withAvailableBalanceChanges ? "1" : "0";
    }
}