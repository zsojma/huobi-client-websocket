using System;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Messages.Account;

namespace Huobi.Client.Websocket.Authentication
{
    public interface IHuobiAuthenticationRequestFactory
    {
        AuthenticationRequest CreateRequest(HuobiAccountWebsocketClientConfig config);
        AuthenticationRequest CreateRequest(Uri uri, string accessKey, string secretKey);
    }
}