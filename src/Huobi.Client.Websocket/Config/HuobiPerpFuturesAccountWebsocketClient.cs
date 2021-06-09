namespace Huobi.Client.Websocket.Config
{
    public class HuobiPerpFuturesAccountWebsocketClient : HuobiGenericWebsocketClientConfig
    {
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
    }
}