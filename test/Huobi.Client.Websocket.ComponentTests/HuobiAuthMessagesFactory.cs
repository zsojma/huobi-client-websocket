﻿namespace Huobi.Client.Websocket.ComponentTests
{
    public static class HuobiAuthMessagesFactory
    {
        public static string CreateAuthenticationResponseMessage()
        {
            var message = @"{
    ""action"": ""req"",
    ""code"": 200,
    ""ch"": ""auth"",
    ""data"": {}
}";
            return message;
        }

        public static string CreateAuthErrorMessage()
        {
            var message = @"{
    ""code"": 2002,
    ""ch"": ""auth"",
    ""message"": ""auth.fail""
}";
            return message;
        }
    }
}