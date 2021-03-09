using System;
using Huobi.Client.Websocket.Messages;

namespace Huobi.Client.Websocket.Client
{
    public interface IHuobiWebsocketClient : IDisposable
    {
        HuobiClientStreams Streams { get; }
        void Start();
        void Send(string message);
        void Send(RequestBase request);
    }
}