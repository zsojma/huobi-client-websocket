using System;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages;

namespace Huobi.Client.Websocket.Client
{
    public interface IHuobiWebsocketClient : IDisposable
    {
        IHuobiWebsocketCommunicator Communicator { get; }
        HuobiClientStreams Streams { get; }
        void Send(RequestBase request);
        void Send(string message);
    }
}