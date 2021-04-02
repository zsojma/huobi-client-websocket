using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.Account.Subscription.OrderUpdates;
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
            await Task.Delay(1000);

            var tasks = new[]
            {
                OrderUpdatesExample(symbol, executionTimeMs)
            };

            await Task.WhenAll(tasks);
        }

        private void SubscribeToStreams()
        {
            _client.Streams.UnhandledMessageStream.Subscribe(x => _logger.LogError($"Unhandled message: {x}"));
            _client.Streams.ErrorMessageStream.Subscribe(
                x => _logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x.Message}"));
            _client.Streams.AuthenticationResponseStream.Subscribe(x => _logger.LogInformation($"Authenticated with response code: {x.Code}"));
            _client.Streams.SubscribeResponseStream.Subscribe(x => _logger.LogInformation($"Subscribed to channel: {x.Channel}"));
        }

        private async Task OrderUpdatesExample(string symbol, int executionTimeMs)
        {
            var marketCandlestickSubscribeRequest = new AccountOrderUpdatesSubscribeRequest(symbol);
            _client.Send(marketCandlestickSubscribeRequest);

            await Task.Delay(executionTimeMs);
        }
    }
}