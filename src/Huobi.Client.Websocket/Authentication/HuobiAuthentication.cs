using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Huobi.Client.Websocket.Utils;
using Microsoft.AspNetCore.Http;
using NodaTime;

namespace Huobi.Client.Websocket.Authentication
{
    public class HuobiAuthentication : IHuobiAuthentication
    {
        private const string METHOD = "GET";
        private const string NEW_LINE_CHAR = "\n";
        private const string ACCESS_KEY_NAME = "accessKey";
        private const string TIMESTAMP_NAME = "timestamp";

        private const string SIGNATURE_METHOD_NAME = "signatureMethod";
        internal const string SIGNATURE_METHOD_VALUE = "HmacSHA256";

        private const string SIGNATURE_VERSION_NAME = "signatureVersion";
        internal const string SIGNATURE_VERSION_VERSION = "2.1";

        public string GenerateSignature(string accessKey, string secretKey, string host, string uri, ZonedDateTime timestamp)
        {
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            using var hmacSha256 = new HMACSHA256(secretKeyBytes);

            var requestString = CreateRequestString(accessKey, host, uri, timestamp);
            var requestStringBytes = Encoding.UTF8.GetBytes(requestString);
            var requestStringHash = hmacSha256.ComputeHash(requestStringBytes);

            var signature = Convert.ToBase64String(requestStringHash);
            return signature;
        }

        private static string CreateRequestString(string accessKey, string host, string uri, ZonedDateTime timestamp)
        {
            var requestStringBuilder = new StringBuilder(METHOD);
            requestStringBuilder.Append(NEW_LINE_CHAR);
            requestStringBuilder.Append(host);
            requestStringBuilder.Append(NEW_LINE_CHAR);
            requestStringBuilder.Append(uri);
            requestStringBuilder.Append(NEW_LINE_CHAR);

            var urlParams = CreateQueryString(accessKey, timestamp);
            requestStringBuilder.Append(urlParams);

            var requestString = requestStringBuilder.ToString();
            return requestString;
        }

        private static string CreateQueryString(string accessKey, ZonedDateTime timestamp)
        {
            var urlParamsDict = new Dictionary<string, string>
            {
                { ACCESS_KEY_NAME, accessKey },
                { SIGNATURE_METHOD_NAME, SIGNATURE_METHOD_VALUE },
                { SIGNATURE_VERSION_NAME, SIGNATURE_VERSION_VERSION },
                { TIMESTAMP_NAME, timestamp.ToHuobiUtcString() }
            };

            var urlParams = QueryString.Create(urlParamsDict);
            return urlParams.Value[1..]; // removes initial question mark
        }
    }
}