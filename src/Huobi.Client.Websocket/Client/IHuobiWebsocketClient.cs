using System;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.MarketData;

namespace Huobi.Client.Websocket.Client
{
    public interface IHuobiWebsocketClient : IDisposable
    {
        IHuobiWebsocketCommunicator Communicator { get; }
        HuobiClientStreams Streams { get; }
        void Send(RequestBase request);
        void Send(AuthRequestBase request);
        void Send(string message);
    }
}