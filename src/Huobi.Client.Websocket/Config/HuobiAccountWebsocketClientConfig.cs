namespace Huobi.Client.Websocket.Config
{
    public class HuobiAccountWebsocketClientConfig : HuobiWebsocketClientConfig
    {
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
    }
}