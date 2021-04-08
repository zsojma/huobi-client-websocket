using System;

namespace Huobi.Client.Websocket.ComponentTests
{
    public static class HuobiMessagesFactory
    {
        public static string CreateSubscribeResponseMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""id"": ""id1"",
  ""status"": ""ok"",
  ""subbed"": ""market.ethbtc.kline.1min"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @"
}";
            return message;
        }

        public static string CreateUnsubscribeResponseMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""id"": ""id1"",
  ""status"": ""ok"",
  ""unsubbed"": ""market.ethbtc.kline.1min"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @"
}";
            return message;
        }

        public static string CreateErrorMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""status"": ""error"",
  ""ts"": """ + timestamp.ToUnixTimeMilliseconds() + @""",
  ""err-code"": ""error code 123"",
  ""err-msg"": ""error message 456""
}";
            return message;
        }

        public static string CreateMarketCandlestickUpdateMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.ethbtc.kline.1min"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""id"": 1489464480,
    ""amount"": 10.11,
    ""count"": 12,
    ""open"": 7962.62,
    ""close"": 7962.63,
    ""low"": 7962.64,
    ""high"": 7962.65,
    ""vol"": 102.30
  }
}";
            return message;
        }

        public static string CreateMarketCandlestickPullResponseMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""id"": ""id1"",
  ""status"": ""ok"",
  ""rep"": ""market.ethbtc.kline.1min"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""data"": [
    {
      ""id"": 1489464480,
      ""amount"": 10.11,
      ""count"": 12,
      ""open"": 7962.62,
      ""close"": 7962.63,
      ""low"": 7962.64,
      ""high"": 7962.65,
      ""vol"": 102.30
    },
    {
      ""id"": 1489464481,
      ""amount"": 11.11,
      ""count"": 13,
      ""open"": 7963.62,
      ""close"": 7963.63,
      ""low"": 7963.64,
      ""high"": 7963.65,
      ""vol"": 103.30
    }
  ]
}";
            return message;
        }

        public static string CreateMarketDepthUpdateMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.btcusdt.depth.step0"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""bids"": [
      [3.7721, 344.86],
      [3.7709, 46.66]
    ],
    ""asks"": [
      [3.7745, 15.44],
      [3.7746, 70.52]
    ],
    ""version"": 100434317651,
    ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @"
  }
}";
            return message;
        }

        public static string CreateMarketDepthPullResponseMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""id"": ""id1"",
  ""status"": ""ok"",
  ""rep"": ""market.btcusdt.depth.step0"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""data"": {
    ""bids"": [
      [3.7721, 344.86],
      [3.7709, 46.66]
    ],
    ""asks"": [
      [3.7745, 15.44],
      [3.7746, 70.52]
    ],
    ""version"": 100434317651,
    ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @"
  }
}";
            return message;
        }

        public static string CreateMarketByPriceUpdateMessage_Asks(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.btcusdt.mbp.5"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""seqNum"": 100020146795,
    ""prevSeqNum"": 100020146794,
    ""asks"": [
      [645.140000000000000000, 26.755973959140651643]
    ]
  }
}";
            return message;
        }
        
        public static string CreateMarketByPriceUpdateMessage_Bids(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.btcusdt.mbp.5"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""seqNum"": 100020146795,
    ""prevSeqNum"": 100020146794,
    ""bids"": [
      [645.140000000000000000, 26.755973959140651643]
    ]
  }
}";
            return message;
        }
        
        public static string CreateMarketByPriceRefreshUpdateMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.btcusdt.mbp.refresh.20"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""seqNum"": 100020142010,
    ""bids"": [
        [618.37, 71.594],
        [423.33, 77.726],
        [223.18, 47.997],
        [219.34, 24.82],
        [210.34, 94.463]
    ],
    ""asks"": [
        [650.59, 14.909733438479636],
        [650.63, 97.996],
        [650.77, 97.465],
        [651.23, 83.973],
        [651.42, 34.465]
    ]
  }
}";
            return message;
        }
        
        public static string CreateMarketByPricePullResponseMessage()
        {
            var message = @"{
    ""id"": ""id2"",
    ""rep"": ""market.btcusdt.mbp.5"",
    ""status"": ""ok"",
    ""data"": {
        ""seqNum"": 100020142010,
        ""bids"": [
            [618.37, 71.594],
            [423.33, 77.726],
            [223.18, 47.997],
            [219.34, 24.82],
            [210.34, 94.463]
        ],
        ""asks"": [
            [650.59, 14.909733438479636],
            [650.63, 97.996],
            [650.77, 97.465],
            [651.23, 83.973],
            [651.42, 34.465]
        ]
    }
}";
            return message;
        }

        public static string CreateMarketBestBidOfferUpdateMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.btcusdt.bbo"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""symbol"": ""btcusdt"",
    ""quoteTime"": 1489474082811,
    ""bid"": 10008.31,
    ""bidSize"": 0.01,
    ""ask"": 10009.54,
    ""askSize"": 0.3,
    ""seqId"": 1276823698734
  }
}";
            return message;
        }

        public static string CreateMarketTradeDetailUpdateMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.btcusdt.trade.detail"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""id"": 14650745135,
    ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
    ""data"": [
      {
        ""amount"": 0.0099,
        ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
        ""id"": 146507451359183894799,
        ""tradeId"": 102043495674,
        ""price"": 401.74,
        ""direction"": ""buy""
      },
      {
        ""amount"": 0.0098,
        ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
        ""id"": 146507451359183894794,
        ""tradeId"": 102043495675,
        ""price"": 402.74,
        ""direction"": ""sell""
      }
    ]
  }
}";
            return message;
        }

        public static string CreateMarketTradeDetailPullResponseMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""id"": ""id1"",
  ""status"": ""ok"",
  ""rep"": ""market.btcusdt.trade.detail"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""data"": [
    {
      ""amount"": 0.0099,
      ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
      ""id"": 146507451359183894799,
      ""tradeId"": 102043495674,
      ""price"": 401.74,
      ""direction"": ""buy""
    },
    {
      ""amount"": 0.0098,
      ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
      ""id"": 146507451359183894794,
      ""tradeId"": 102043495675,
      ""price"": 402.74,
      ""direction"": ""sell""
    }
  ]
}";
            return message;
        }

        public static string CreateMarketDetailsUpdateMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""ch"": ""market.btcusdt.detail"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""tick"": {
    ""amount"": 12224.2922,
    ""open"":   9790.52,
    ""close"":  10195.00,
    ""high"":   10300.00,
    ""id"":     1494496390,
    ""count"":  15195,
    ""low"":    9657.00,
    ""vol"":    121906001.754751
  }
}";
            return message;
        }

        public static string CreateMarketDetailsPullResponseMessage(DateTimeOffset timestamp)
        {
            var message = @"{
  ""id"": ""id1"",
  ""status"": ""ok"",
  ""rep"": ""market.btcusdt.detail"",
  ""ts"": " + timestamp.ToUnixTimeMilliseconds() + @",
  ""data"": {
    ""amount"": 12224.2922,
    ""open"":   9790.52,
    ""close"":  10195.00,
    ""high"":   10300.00,
    ""id"":     1494496390,
    ""count"":  15195,
    ""low"":    9657.00,
    ""vol"":    121906001.754751
  }
}";
            return message;
        }
    }
}