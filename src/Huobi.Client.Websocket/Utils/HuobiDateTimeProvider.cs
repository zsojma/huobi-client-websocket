using System;

namespace Huobi.Client.Websocket.Utils;

public class HuobiDateTimeProvider : IHuobiDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}