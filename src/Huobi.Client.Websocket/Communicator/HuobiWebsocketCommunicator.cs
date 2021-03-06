﻿using System;
using Huobi.Client.Websocket.Config;
using Microsoft.Extensions.Options;
using Websocket.Client;

namespace Huobi.Client.Websocket.Communicator
{
    internal class HuobiWebsocketCommunicator : WebsocketClient, IHuobiWebsocketCommunicator
    {
        public HuobiWebsocketCommunicator(IOptions<HuobiWebsocketClientConfig> config)
            : base(new Uri(config.Value.Url))
        {
            Name = config.Value.Name;
            ReconnectTimeout = TimeSpan.FromMinutes(config.Value.ReconnectTimeoutMinutes);
        }
    }
}