using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account;

public class AccountMessageData
{
    public AccountMessageData(DateTimeOffset timestamp)
    {
        Timestamp = timestamp;
    }
        
    [JsonProperty("ts")]
    public DateTimeOffset Timestamp { get; }
}