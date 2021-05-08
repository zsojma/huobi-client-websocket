using System;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Utils;

namespace Huobi.Client.Websocket.Authentication
{
    public class HuobiAuthenticationRequestFactory : IHuobiAuthenticationRequestFactory
    {
        private readonly IHuobiDateTimeProvider _dateTimeProvider;
        private readonly IHuobiSignature _signature;

        public HuobiAuthenticationRequestFactory(
            IHuobiDateTimeProvider dateTimeProvider,
            IHuobiSignature signature)
        {
            _dateTimeProvider = dateTimeProvider;
            _signature = signature;
        }

        public AuthenticationRequest CreateRequest(HuobiAccountWebsocketClientConfig config)
        {
            Validations.ValidateInput(config.Url, nameof(config.Url));
            Validations.ValidateInput(config.AccessKey, nameof(config.AccessKey));
            Validations.ValidateInput(config.SecretKey, nameof(config.SecretKey));
            
            var uri = new Uri(config.Url!);
            return CreateRequest(uri, config.AccessKey!, config.SecretKey!);
        }

        public AuthenticationRequest CreateRequest(Uri uri, string accessKey, string secretKey)
        {
            var now = _dateTimeProvider.UtcNow;
            var signature = _signature.Create(
                accessKey,
                secretKey,
                uri.Host,
                uri.LocalPath,
                now);
            return new AuthenticationRequest(accessKey, signature, now);
        }
    }
}