namespace Huobi.Client.Websocket.ComponentTests
{
    public static class HuobiMessagesFactory
    {
        public static string CreateSubscribeResponseMessage()
        {
            var message = @"{
  ""id"": ""id1"",
  ""status"": ""ok"",
  ""subbed"": ""market.ethbtc.kline.1min"",
  ""ts"": 1489474081631
}";
            return message;
        }

        public static string CreateMarketCandlestickUpdateMessage()
        {
            var message = @"{
  ""ch"": ""market.ethbtc.kline.1min"",
  ""ts"": 1489474082831,
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

        public static string CreateMarketCandlestickPullMessage()
        {
            var message = @"{
  ""status"": ""ok"",
  ""rep"": ""market.ethbtc.kline.1min"",
  ""ts"": 1489474082831,
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

        public static string CreateMarketDepthUpdateMessage()
        {
            var message = @"{
  ""ch"": ""market.btcusdt.depth.step0"",
  ""ts"": 1572362902027,
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
    ""ts"": 1572362902012
  }
}";
            return message;
        }

        public static string CreateMarketDepthPullMessage()
        {
            var message = @"{
  ""status"": ""ok"",
  ""rep"": ""market.btcusdt.depth.step0"",
  ""ts"": 1572362902027,
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
    ""ts"": 1572362902012
  }
}";
            return message;
        }

        public static string CreateMarketBestBidOfferUpdateMessage()
        {
            var message = @"{
  ""ch"": ""market.btcusdt.bbo"",
  ""ts"": 1489474082831,
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

        public static string CreateMarketTradeDetailUpdateMessage()
        {
            var message = @"{
  ""ch"": ""market.btcusdt.trade.detail"",
  ""ts"": 1489474082831,
  ""tick"": {
    ""id"": 14650745135,
    ""ts"": 1533265950234, //trade time
    ""data"": [
      {
        ""amount"": 0.0099,
        ""ts"": 1533265950234,
        ""id"": 146507451359183894799,
        ""tradeId"": 102043495674,
        ""price"": 401.74,
        ""direction"": ""buy""
      },
      {
        ""amount"": 0.0098,
        ""ts"": 1533265950235,
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

        public static string CreateMarketTradeDetailPullMessage()
        {
            var message = @"{
  ""status"": ""ok"",
  ""rep"": ""market.btcusdt.trade.detail"",
  ""ts"": 1489474082831,
  ""data"": [
    {
      ""amount"": 0.0099,
      ""ts"": 1533265950234,
      ""id"": 146507451359183894799,
      ""tradeId"": 102043495674,
      ""price"": 401.74,
      ""direction"": ""buy""
    },
    {
      ""amount"": 0.0098,
      ""ts"": 1533265950235,
      ""id"": 146507451359183894794,
      ""tradeId"": 102043495675,
      ""price"": 402.74,
      ""direction"": ""sell""
    }
  ]
}";
            return message;
        }
    }
}