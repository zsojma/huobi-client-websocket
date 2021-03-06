namespace Huobi.Client.Websocket.Config
{
    public class HuobiWebsocketClientConfig
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int ReconnectTimeoutMinutes { get; set; }
    }
}