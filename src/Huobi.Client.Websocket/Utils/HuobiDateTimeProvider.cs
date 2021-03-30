using System;
using NodaTime;

namespace Huobi.Client.Websocket.Utils
{
    internal class HuobiDateTimeProvider : IHuobiDateTimeProvider
    {
        public ZonedDateTime UtcNow
        {
            get
            {
                var dateTimeOffset = DateTimeOffset.UtcNow;
                var now = new ZonedDateTime(Instant.FromDateTimeOffset(dateTimeOffset), DateTimeZone.Utc);
                return now;
            }
        }
    }
}