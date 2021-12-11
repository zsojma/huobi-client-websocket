using System;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Utils;
using Microsoft.Extensions.Options;
using Websocket.Client;

namespace Huobi.Client.Websocket.Communicator;

public class HuobiGenericWebsocketCommunicator : WebsocketClient, IHuobiGenericWebsocketCommunicator
{
    public HuobiGenericWebsocketCommunicator(IOptions<HuobiGenericWebsocketClientConfig> config)
        : this(config.Value)
    {
    }

    protected HuobiGenericWebsocketCommunicator(HuobiGenericWebsocketClientConfig config)
        : base(GetUri(config))
    {
        Validations.ValidateInput(config, nameof(config));

        Name = config.CommunicatorName ?? "Huobi";

        if (config.ReconnectTimeoutMin.HasValue)
        {
            ReconnectTimeout = TimeSpan.FromMinutes(config.ReconnectTimeoutMin.Value);
        }
    }

    private static Uri GetUri(HuobiGenericWebsocketClientConfig config)
    {
        Validations.ValidateInput(config, nameof(config));
        Validations.ValidateInput(config.Url, nameof(config.Url));

        return new Uri(config.Url!);
    }
}