using System;
using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Utils;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket.Messages.Account.Factories
{
    public class HuobiAuthenticationRequestFactory : IHuobiAuthenticationRequestFactory
    {
        private readonly IHuobiDateTimeProvider _dateTimeProvider;
        private readonly IHuobiAuthentication _authentication;
        private readonly IOptions<HuobiWebsocketClientConfig> _config;

        private readonly Uri _uri;

        public HuobiAuthenticationRequestFactory(
            IHuobiDateTimeProvider dateTimeProvider,
            IHuobiAuthentication authentication,
            IOptions<HuobiWebsocketClientConfig> config)
        {
            Validations.ValidateInput(config.Value.Url, nameof(config.Value.Url));
            Validations.ValidateInput(config.Value.AccessKey, nameof(config.Value.AccessKey));
            Validations.ValidateInput(config.Value.SecretKey, nameof(config.Value.SecretKey));

            _dateTimeProvider = dateTimeProvider;
            _authentication = authentication;
            _config = config;

            _uri = new Uri(config.Value.Url!);
        }

        public AuthenticationRequest CreateRequest()
        {
            var now = _dateTimeProvider.UtcNow;
            var signature = _authentication.GenerateSignature(
                _config.Value.AccessKey!,
                _config.Value.SecretKey!,
                _uri.Host,
                _uri.LocalPath,
                now);
            return new AuthenticationRequest(_config.Value.AccessKey!, signature, now);
        }
    }
}