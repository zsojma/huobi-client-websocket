using System;

namespace Huobi.Client.Websocket.Utils;

public interface IHuobiDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}