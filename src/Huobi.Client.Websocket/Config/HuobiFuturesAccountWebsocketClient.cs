namespace Huobi.Client.Websocket.Config
{
    public class HuobiFuturesAccountWebsocketClient : HuobiGenericWebsocketClientConfig
    {
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
    }
}