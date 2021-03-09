![Logo](huobi-logo-alt.png)
# Huobi websocket API client

This is a C# implementation of the Huobi websocket API found here:

https://huobiapi.github.io/docs/spot/v1/en/

[Releases and breaking changes](https://github.com/zsojma/huobi-client-websocket/releases)

### License: 
    Apache License 2.0

### Features

* installation via NuGet ([Huobi.Client.Websocket](https://www.nuget.org/packages/Huobi.Client.Websocket))
* public and authenticated API
* targeting .NET Standard 2.0 (.NET Core, Linux/MacOS compatible)
* reactive extensions ([Rx.NET](https://github.com/Reactive-Extensions/Rx.NET))

### Usage

TODO

### API coverage

| PUBLIC                 |    Covered     |  
|------------------------|:--------------:|
| Ping-Pong              |  ✔            |
| Subscribe              |  ✔            |
| Unsubscribe            |  ✔            |
| Market candlestick     |  ✔            |
| Market depth           |  ✔            |
| Market by price        |                |
| Market best bid/offer  |                |
| Market trace           |                |
| Market details         |                |
| Market trace           |                |

| AUTHENTICATED          |    Covered     |  
|------------------------|:--------------:|
| Ping-Pong              |  ✔            |
| Authentication         |                |
| Order updates          |                |
| Trades                 |                |
| Account changes        |                |

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
