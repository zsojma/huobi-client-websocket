using Huobi.Client.Websocket.Clients.Streams;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Clients
{
    public class HuobiGenericWebsocketClient : HuobiWebsocketClientBase<HuobiGenericClientStreams>, IHuobiGenericWebsocketClient
    {
        public HuobiGenericWebsocketClient(
            IHuobiGenericWebsocketCommunicator communicator,
            IHuobiSerializer serializer,
            ILogger<HuobiGenericWebsocketClient> logger)
            : base(communicator, serializer, logger)
        {
        }

        public void Send(object request)
        {
            var serialized = Serializer.Serialize(request);
            base.Send(serialized);
        }

        protected override bool TryHandleMessage(string message)
        {
            Streams.ResponseMessageSubject.OnNext(message);
            return true;
        }
    }
}