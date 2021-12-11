namespace Huobi.Client.Websocket.Config;

public class HuobiAccountWebsocketClientConfig : HuobiGenericWebsocketClientConfig
{
    public string? AccessKey { get; set; }
    public string? SecretKey { get; set; }
}