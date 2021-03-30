using NodaTime;

namespace Huobi.Client.Websocket.Utils
{
    public interface IHuobiDateTimeProvider
    {
        ZonedDateTime UtcNow { get; }
    }
}