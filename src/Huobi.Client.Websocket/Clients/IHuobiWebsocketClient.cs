using System;
using System.Threading.Tasks;

namespace Huobi.Client.Websocket.Clients
{
    public interface IHuobiWebsocketClient<out TStreams, in TRequest> : IDisposable
    {
        TStreams Streams { get; }
        Task Start();
        void Send(TRequest request);
        void Send(string request);
    }
}