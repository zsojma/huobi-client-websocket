namespace Huobi.Client.Websocket.Config
{
    public class HuobiWebsocketClientConfig
    {
        public string? Url { get; set; }
        public string? CommunicatorName { get; set; }
        public int? ReconnectTimeoutMin { get; set; }
    }
}