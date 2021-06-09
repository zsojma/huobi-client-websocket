using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.Account.FuturesLiquidation;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Sample.Examples
{
    public class FuturesClientExample : IExample
    {
        private readonly IHuobiAccountWebsocketClient _client;
        private readonly ILogger<FuturesClientExample> _logger;

        public FuturesClientExample(IHuobiAccountWebsocketClient client, ILogger<FuturesClientExample> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task Start(string symbol)
        {
            SubscribeToStreams();

            await _client.Start();

            var tasks = new[]
            {
                LiquidationsExample("*")
            };

            await Task.WhenAll(tasks);
        }

        public Task Stop(string symbol)
        {
            // nothing to do
            return Task.CompletedTask;
        }

        private void SubscribeToStreams()
        {
            _client.Streams.UnhandledMessageStream.Subscribe(x => _logger.LogError($"Unhandled message: {x}"));
            _client.Streams.ErrorMessageStream.Subscribe(
                x => _logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x}"));
            _client.Streams.AuthenticationResponseStream.Subscribe(
                x => _logger.LogError($"Authenticated with response code: {x.Code}"));
            _client.Streams.SubscribeResponseStream.Subscribe(x => _logger.LogInformation($"Subscribed to channel: {x.Topic}"));

            _client.Streams.FuturesLiquidationMessageStream.Subscribe(Handle);
        }

        private Task LiquidationsExample(string symbol)
        {
            var subscribeRequest = new FuturesLiquidationsSubscribeRequest(symbol);
            _client.Send(subscribeRequest);

            return Task.CompletedTask;
        }
        private void Handle(FuturesLiquidationMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Liquidation | [currency={msg.Data.Symbol}] [contract={msg.Data.Contract_code}] [direction={msg.Data.Direction}] [volume={msg.Data.Volume}] [price={msg.Data.Price}] [amount={msg.Data.Amount}]");
        }
    }
}