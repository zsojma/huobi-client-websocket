using System;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Messages.Account.AccountUpdates;
using Huobi.Client.Websocket.Messages.Account.OrderUpdates;
using Huobi.Client.Websocket.Messages.Account.TradeDetails;
using Huobi.Client.Websocket.Messages.Account.Values;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Sample.Examples
{
    public class AccountClientExample : IExample
    {
        private readonly IHuobiAccountWebsocketClient _client;
        private readonly ILogger<AccountClientExample> _logger;

        public AccountClientExample(IHuobiAccountWebsocketClient client, ILogger<AccountClientExample> logger)
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
                OrderUpdatesExample(symbol),
                TradeDetailsExample(symbol),
                AccountUpdateExample()
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
                x => _logger.LogError($"Error message received! Code: {x.ErrorCode}; Message: {x.Message}"));
            _client.Streams.AuthenticationResponseStream.Subscribe(
                x => _logger.LogInformation($"Authenticated with response code: {x.Code}"));
            _client.Streams.SubscribeResponseStream.Subscribe(x => _logger.LogInformation($"Subscribed to channel: {x.Channel}"));

            _client.Streams.ConditionalOrderTriggerFailureMessageStream.Subscribe(Handle);
            _client.Streams.ConditionalOrderCanceledMessageStream.Subscribe(Handle);
            _client.Streams.OrderSubmittedMessageStream.Subscribe(Handle);
            _client.Streams.OrderTradedMessageStream.Subscribe(Handle);
            _client.Streams.OrderCanceledMessageStream.Subscribe(Handle);

            _client.Streams.TradeDetailsMessageStream.Subscribe(Handle);

            _client.Streams.AccountUpdateMessageStream.Subscribe(Handle);
        }

        private Task OrderUpdatesExample(string symbol)
        {
            var subscribeRequest = new OrderUpdatesSubscribeRequest(symbol);
            _client.Send(subscribeRequest);

            return Task.CompletedTask;
        }

        private Task TradeDetailsExample(string symbol)
        {
            var subscribeRequest = new TradeDetailsSubscribeRequest(symbol);
            _client.Send(subscribeRequest);

            return Task.CompletedTask;
        }

        private Task AccountUpdateExample()
        {
            var subscribeRequest = new AccountUpdateSubscribeRequest(true);
            _client.Send(subscribeRequest);

            return Task.CompletedTask;
        }

        private void Handle(ConditionalOrderTriggerFailureMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Conditional order trigger failure on {msg.Data.Symbol} | [orderSide={msg.Data.OrderSide}] [failureTime={msg.Data.OrderTriggeringFailureTime}] [errorCode={msg.Data.ErrorCode}] [errorMessage={msg.Data.ErrorMessage}]");
        }

        private void Handle(ConditionalOrderCanceledMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Conditional order cancelled on {msg.Data.Symbol} | [orderSide={msg.Data.OrderSide}] [triggerTime={msg.Data.OrderTriggerTime}]");
        }

        private void Handle(OrderSubmittedMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Order submitted on {msg.Data.Symbol} | [orderType={msg.Data.Type}] [price={msg.Data.OrderPrice}] [size={msg.Data.OrderSize}] [orderId={msg.Data.OrderId}]");
        }

        private void Handle(OrderTradedMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Order traded on {msg.Data.Symbol} | [orderType={msg.Data.Type}] [price={msg.Data.OrderPrice}] [size={msg.Data.OrderSize}] [orderId={msg.Data.OrderId}]");
        }

        private void Handle(OrderCanceledMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Order cancelled on {msg.Data.Symbol} | [orderType={msg.Data.Type}] [price={msg.Data.OrderPrice}] [size={msg.Data.OrderSize}] [orderId={msg.Data.OrderId}]");
        }

        private void Handle(TradeDetailsMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                msg.Data.EventType == TradeEventType.Trade
                    ? $"Trade matched on {msg.Data.Symbol} | [orderType={msg.Data.OrderType}] [price={msg.Data.OrderPrice}] [size={msg.Data.OrderSize}] [orderId={msg.Data.OrderId}]"
                    : $"Trade canceled on {msg.Data.Symbol} | [orderType={msg.Data.OrderType}] [price={msg.Data.OrderPrice}] [size={msg.Data.OrderSize}] [orderId={msg.Data.OrderId}]");
        }

        private void Handle(AccountUpdateMessage msg)
        {
            if (msg.Data is null)
            {
                return;
            }

            _logger.LogInformation(
                $"Account updated | [currency={msg.Data.Currency}] [balance={msg.Data.Balance}] [available={msg.Data.Available}] [changeType={msg.Data.ChangeType}] [accountType={msg.Data.AccountType}]");
        }
    }
}