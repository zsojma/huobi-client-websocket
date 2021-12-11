namespace Huobi.Client.Websocket.Messages.Account.Values;

public enum OrderEventType
{
    Unknown,
    Trigger,
    Deletion,
    Creation,
    Trade,
    Cancellation
}