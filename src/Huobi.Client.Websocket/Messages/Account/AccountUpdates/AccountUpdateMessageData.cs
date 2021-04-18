using System;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.AccountUpdates
{
    public class AccountUpdateMessageData
    {
        [JsonConstructor]
        public AccountUpdateMessageData(
            string changeTypeStr,
            string accountTypeStr,
            string currency,
            long accountId,
            decimal balance,
            decimal available,
            DateTimeOffset changeTime)
        {
            ChangeTypeStr = changeTypeStr;
            AccountTypeStr = accountTypeStr;

            Currency = currency;
            AccountId = accountId;
            Balance = balance;
            Available = available;
            ChangeTime = changeTime;
        }

        [JsonIgnore]
        public AccountChangeType? ChangeType => AccountChangeTypeHelper.FromNullableMessageValue(ChangeTypeStr);

        [JsonIgnore]
        public AccountType AccountType => AccountTypeHelper.FromMessageValue(AccountTypeStr);
        
        public string Currency { get; }
        public long AccountId { get; }
        public decimal Balance { get; }
        public decimal Available { get; }
        
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset ChangeTime { get; }

        [JsonProperty("changeType")]
        internal string ChangeTypeStr { get; }

        [JsonProperty("accountType")]
        internal string AccountTypeStr { get; }
    }
}