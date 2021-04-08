using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountMessageData
    {
        public AccountMessageData(long timestampMs)
        {
            TimestampMs = timestampMs;
        }
        
        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);
        
        [JsonProperty("ts")]
        internal long TimestampMs { get; }
    }
}