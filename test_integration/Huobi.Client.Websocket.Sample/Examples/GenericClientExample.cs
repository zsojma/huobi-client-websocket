using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Sample.Examples
{
    internal class GenericClientExample
    {
        private readonly IHuobiGenericWebsocketClient _client;
        private readonly ILogger<GenericClientExample> _logger;

        public GenericClientExample(IHuobiGenericWebsocketClient client, ILogger<GenericClientExample> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task Execute(string symbol, int executionTimeMs = 10000)
        {
            SubscribeToStreams();
            
            await _client.Start();

            var subscribeRequest = new MarketCandlestickSubscribeRequest(symbol, MarketCandlestickPeriodType.FiveMinutes, "id1");
            _client.Send(subscribeRequest);

            await Task.Delay(executionTimeMs);
            
            var unsubscribeRequest = new MarketCandlestickUnsubscribeRequest(symbol, MarketCandlestickPeriodType.FiveMinutes, "id1");
            _client.Send(unsubscribeRequest);
        }

        private void SubscribeToStreams()
        {
            _client.Streams.UnhandledMessageStream.Subscribe(x => _logger.LogError($"Unhandled message: {x}"));
            _client.Streams.ErrorMessageStream.Subscribe(
                x => _logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x.Message}"));
            
            _client.Streams.ResponseMessageStream.Subscribe(x => _logger.LogInformation($"Response message: {x}"));
        }
    }
}