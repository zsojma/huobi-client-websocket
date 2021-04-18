using System;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountMessageData
    {
        public AccountMessageData(DateTimeOffset timestamp)
        {
            Timestamp = timestamp;
        }
        
        [JsonProperty("ts")]
        [JsonConverter(typeof(UnitMillisecondsToDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; }
    }
}