using System;

namespace Huobi.Client.Websocket.ComponentTests
{
    public static class TestUtils
    {
        public static bool UnixTimesEqual(DateTimeOffset o1, DateTimeOffset o2)
        {
            var ticks1 = o1.ToUnixTimeMilliseconds();
            var ticks2 = o2.ToUnixTimeMilliseconds();
            return ticks1 == ticks2;
        }
    }
}