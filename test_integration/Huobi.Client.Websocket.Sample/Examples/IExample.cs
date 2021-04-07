using System.Threading.Tasks;

namespace Huobi.Client.Websocket.Sample.Examples
{
    public interface IExample
    {
        Task Start(string symbol);
        Task Stop(string symbol);
    }
}