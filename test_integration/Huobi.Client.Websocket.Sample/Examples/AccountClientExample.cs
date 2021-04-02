using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.MarketData.Subscription.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Sample.Examples
{
    public class AccountClientExample
    {
        private readonly IHuobiAccountWebsocketClient _client;
        private readonly ILogger<AccountClientExample> _logger;

        public AccountClientExample(IHuobiAccountWebsocketClient client, ILogger<AccountClientExample> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task Execute(string symbol, int executionTimeMs = 10000)
        {
            SubscribeToStreams();
            
            await _client.Start();

            //var request = new MarketCandlestickSubscribeRequest(symbol, MarketCandlestickPeriodType.FiveMinutes, "id1");
            //_client.Send(request);
        }

        private void SubscribeToStreams()
        {
            _client.Streams.UnhandledMessageStream.Subscribe(x => _logger.LogError($"Unhandled message: {x}"));
            _client.Streams.ErrorMessageStream.Subscribe(
                x => _logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x.Message}"));
            
            //_client.Streams.ResponseMessageStream.Subscribe(x => _logger.LogInformation($"Response message: {x}"));
        }
    }
}