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
            _dateTimeProvider = dateTimeProvider;
            _authentication = authentication;
            _config = config;

            _uri = new Uri(config.Value.Url ?? string.Empty);
        }

        public AuthenticationRequest CreateRequest()
        {
            var now = _dateTimeProvider.UtcNow;
            var signature = _authentication.GenerateSignature(
                _config.Value.AccessKey,
                _config.Value.SecretKey,
                _uri.Host,
                _uri.LocalPath,
                now);
            return new AuthenticationRequest(_config.Value.AccessKey, signature, now);
        }
    }
}