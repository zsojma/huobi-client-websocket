using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.MarketData;
using Websocket.Client;
using Websocket.Client.Models;

namespace Huobi.Client.Websocket.Clients.Streams;

public class HuobiClientStreamsBase
{
    public readonly Subject<ReconnectionInfo> ReconnectionInfoStream = new();
    public readonly Subject<DisconnectionInfo> DisconnectionInfoStream = new();
    public readonly Subject<string> UnhandledMessageStream = new();
    public readonly Subject<ErrorMessage> ErrorMessageStream = new();
    public readonly Subject<PingRequest> PingMessageStream = new();
    public readonly Subject<AccountErrorMessage> AccountErrorMessageStream = new();
    public readonly Subject<AccountPingRequest> AccountPingMessageStream = new();
}