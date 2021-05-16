using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Communicator;

namespace Huobi.Client.Websocket.Clients
{
    public interface IHuobiWebsocketClient<out TStreams, in TRequest> : IDisposable
    {
        IHuobiGenericWebsocketCommunicator Communicator { get; }
        TStreams Streams { get; }
        Task Start();
        void Send(TRequest request);
        void Send(string request);
    }
}