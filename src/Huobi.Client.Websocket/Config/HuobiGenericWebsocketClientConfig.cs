namespace Huobi.Client.Websocket.Config
{
    public class HuobiGenericWebsocketClientConfig
    {
        public string? Url { get; set; }
        public string? CommunicatorName { get; set; }
        public int? ReconnectTimeoutMin { get; set; }
    }
}