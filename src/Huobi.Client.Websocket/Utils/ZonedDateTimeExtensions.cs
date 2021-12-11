using System;

namespace Huobi.Client.Websocket.Utils;

public static class ZonedDateTimeExtensions
{
    public static string ToHuobiUtcString(this DateTimeOffset dateTime)
    {
        return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss");
    }
}