using NodaTime;

namespace Huobi.Client.Websocket.Utils
{
    public static class ZonedDateTimeExtensions
    {
        public static string ToHuobiUtcString(this ZonedDateTime dateTime)
        {
            return dateTime.ToDateTimeUtc().ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}