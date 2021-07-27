![Logo](huobi-logo-alt.png)
# Huobi websocket API client [![Build status](https://github.com/zsojma/huobi-client-websocket/actions/workflows/deployment.yml/badge.svg?branch=master)](https://github.com/zsojma/huobi-client-websocket/actions/workflows/continuous-deployment.yml) [![NuGet version](https://badge.fury.io/nu/Huobi.Client.Websocket.svg)](https://badge.fury.io/nu/Huobi.Client.Websocket) [![Nuget download](https://img.shields.io/nuget/dt/Huobi.Client.Websocket)](https://www.nuget.org/packages/Huobi.Client.Websocket)

This is a C# implementation of the Huobi websocket API found here:

https://huobiapi.github.io/docs/spot/v1/en/

[Releases and breaking changes](https://github.com/zsojma/huobi-client-websocket/releases)

### License: 
    Apache License 2.0

### Features

* installation via NuGet ([Huobi.Client.Websocket](https://www.nuget.org/packages/Huobi.Client.Websocket))
* public and authenticated API
* targeting .NET Standard 2.0 (.NET Core, Windows/Linux/MacOS compatible)
* reactive extensions ([Rx.NET](https://github.com/Reactive-Extensions/Rx.NET))

### Usage

```csharp
var exitEvent = new ManualResetEvent(false);
var url = HuobiConstants.ApiWebsocketUrl;

using var client = HuobiWebsocketClientsFactory.CreateMarketClient(url);
client.Streams.TradeDetailUpdateStream.Subscribe(
    msg =>
    {
        for (var i = 0; i < msg.Tick?.Data.Length; ++i)
        {
            var item = msg.Tick.Data[i];

            Console.WriteLine(
                $"Market trade detail update {msg.Topic}"
              + $" | [item {i}: amount={item.Amount} price={item.Price} direction={item.Direction}]");
        }
    });

var subscribeRequest = new MarketTradeDetailSubscribeRequest("id1", "btcusdt");
client.Send(subscribeRequest);

await client.Start();

exitEvent.WaitOne(TimeSpan.FromSeconds(30));
```

There are three types of clients where each connects to different Huobi API to get related data:
* **Market client** ([src](src/Huobi.Client.Websocket/Clients/HuobiMarketWebsocketClient.cs)) - Market data from *wss://api.huobi.pro/ws*
* **MarketByPrice client** ([src](src/Huobi.Client.Websocket/Clients/HuobiMarketByPriceWebsocketClient.cs)) - MBP incremental data from *wss://api.huobi.pro/feed*
* **Account client** ([src](src/Huobi.Client.Websocket/Clients/HuobiAccountWebsocketClient.cs)) - Assets and Order data from *wss://api.huobi.pro/ws/v2*

More usage examples:
* integration tests ([link](test_integration/Huobi.Client.Websocket.Sample))
  * you can setup which client to run by registration to DI container [here](https://github.com/zsojma/huobi-client-websocket/blob/master/test_integration/Huobi.Client.Websocket.Sample/Setup.cs#L31)

### API coverage

| PUBLIC                 |    Covered     |
|------------------------|:--------------:|
| Ping-Pong              |  ✔            |
| Subscribe              |  ✔            |
| Unsubscribe            |  ✔            |
| Market candlestick     |  ✔            |
| Market depth           |  ✔            |
| Market by price        |  ✔            |
| Market best bid/offer  |  ✔            |
| Market trade detail    |  ✔            |
| Market details         |  ✔            |
| Errors                 |  ✔            |

| AUTHENTICATED          |    Covered     |
|------------------------|:--------------:|
| Ping-Pong              |  ✔            |
| Authentication         |  ✔            |
| Order updates          |  ✔            |
| Trade details          |  ✔            |
| Account updates        |  ✔            |

**Pull Requests are welcome!**

### Other websocket libraries

<table>
<tr>

<td>
<a href="https://github.com/Marfusios/crypto-websocket-extensions"><img src="https://raw.githubusercontent.com/Marfusios/crypto-websocket-extensions/master/cwe_logo.png" height="80px"></a>
<br />
<a href="https://github.com/Marfusios/crypto-websocket-extensions">Extensions</a>
<br />
<span>All order books together, etc.</span>
</td>

<td>
<a href="https://github.com/Marfusios/bitmex-client-websocket"><img src="https://user-images.githubusercontent.com/1294454/27766319-f653c6e6-5ed4-11e7-933d-f0bc3699ae8f.jpg"></a>
<br />
<a href="https://github.com/Marfusios/bitmex-client-websocket">Bitmex</a>
</td>

<td>
<a href="https://github.com/Marfusios/bitfinex-client-websocket"><img src="https://user-images.githubusercontent.com/1294454/27766244-e328a50c-5ed2-11e7-947b-041416579bb3.jpg"></a>
<br />
<a href="https://github.com/Marfusios/bitfinex-client-websocket">Bitfinex</a>
</td>

<td>
<a href="https://github.com/Marfusios/binance-client-websocket"><img src="https://user-images.githubusercontent.com/1294454/29604020-d5483cdc-87ee-11e7-94c7-d1a8d9169293.jpg"></a>
<br />
<a href="https://github.com/Marfusios/binance-client-websocket">Binance</a>
</td>

<td>
<a href="https://github.com/Marfusios/coinbase-client-websocket"><img src="https://user-images.githubusercontent.com/1294454/41764625-63b7ffde-760a-11e8-996d-a6328fa9347a.jpg"></a>
<br />
<a href="https://github.com/Marfusios/coinbase-client-websocket">Coinbase Pro</a>
</td>

</tr>
</table>
