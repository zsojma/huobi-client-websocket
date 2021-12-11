using System.Reactive.Subjects;

namespace Huobi.Client.Websocket.Clients.Streams;

public class HuobiGenericClientStreams : HuobiClientStreamsBase
{
    public readonly Subject<string> ResponseMessageStream = new();
}