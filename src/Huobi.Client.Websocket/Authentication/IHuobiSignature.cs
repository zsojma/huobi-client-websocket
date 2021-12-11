using System;

namespace Huobi.Client.Websocket.Authentication;

public interface IHuobiSignature
{
    string Create(string accessKey, string secretKey, string host, string uri, DateTimeOffset timestamp);
}