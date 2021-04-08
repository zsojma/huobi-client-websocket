using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.AccountUpdates
{
    public class AccountUpdateMessageData
    {
        [JsonConstructor]
        public AccountUpdateMessageData(
            string? changeTypeStr,
            string accountTypeStr,
            long? changeTimeMs,
            string currency,
            long accountId,
            decimal balance,
            decimal available)
        {
            ChangeTypeStr = changeTypeStr;
            AccountTypeStr = accountTypeStr;
            ChangeTimeMs = changeTimeMs;

            Currency = currency;
            AccountId = accountId;
            Balance = balance;
            Available = available;
        }

        [JsonIgnore]
        public AccountChangeType? ChangeType => AccountChangeTypeHelper.FromNullableMessageValue(ChangeTypeStr);

        [JsonIgnore]
        public AccountType AccountType => AccountTypeHelper.FromMessageValue(AccountTypeStr);

        [JsonIgnore]
        public DateTimeOffset? ChangeTime =>
            ChangeTimeMs.HasValue
                ? DateTimeOffset.FromUnixTimeMilliseconds(ChangeTimeMs.Value)
                : null;

        public string Currency { get; }
        public long AccountId { get; }
        public decimal Balance { get; }
        public decimal Available { get; }

        [JsonProperty("changeType")]
        internal string? ChangeTypeStr { get; }

        [JsonProperty("accountType")]
        internal string AccountTypeStr { get; }

        [JsonProperty("changeTime")]
        public long? ChangeTimeMs { get; }
    }
}