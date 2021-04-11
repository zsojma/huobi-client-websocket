namespace Huobi.Client.Websocket
{
    public class HuobiConstants
    {
        public const string ApiWebsocketUrl = "wss://api.huobi.pro/ws";
        public const string AwsApiWebsocketUrl = "wss://api-aws.huobi.pro/ws";
        public const string ApiMbpWebsocketUrl = "wss://api.huobi.pro/feed";
        public const string AwsApiMpbWebsocketUrl = "wss://api-aws.huobi.pro/feed";
        public const string ApiAccountWebsocketUrl = "wss://api.huobi.pro/ws/v2";
        public const string AwsApiAccountWebsocketUrl = "wss://api-aws.huobi.pro/ws/v2";
        
        internal static readonly string MARKET_PREFIX = "market";
    }
}