using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.AccountUpdates;

public class AccountUpdateMessageData
{
    [JsonConstructor]
    public AccountUpdateMessageData(
        string currency,
        long accountId,
        decimal balance,
        decimal available,
        AccountChangeType changeType,
        AccountType accountType,
        DateTimeOffset changeTime)
    {
        Currency = currency;
        AccountId = accountId;
        Balance = balance;
        Available = available;
        ChangeType = changeType;
        AccountType = accountType;
        ChangeTime = changeTime;
    }

    public string Currency { get; }
    public long AccountId { get; }
    public decimal Balance { get; }
    public decimal Available { get; }
    public AccountChangeType ChangeType { get; }
    public AccountType AccountType { get; }
    public DateTimeOffset ChangeTime { get; }
}