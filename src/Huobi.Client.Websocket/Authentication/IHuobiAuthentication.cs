using NodaTime;

namespace Huobi.Client.Websocket.Authentication
{
    public interface IHuobiAuthentication
    {
        string GenerateSignature(string accessKey, string secretKey, string host, string uri, ZonedDateTime timestamp);
    }
}