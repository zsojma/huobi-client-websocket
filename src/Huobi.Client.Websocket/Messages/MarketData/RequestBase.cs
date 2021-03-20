﻿using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public abstract class RequestBase
    {
        protected RequestBase(string? reqId = null)
        {
            ReqId = reqId;
        }

        [JsonProperty("id")]
        public string? ReqId { get; internal set; }
    }
}