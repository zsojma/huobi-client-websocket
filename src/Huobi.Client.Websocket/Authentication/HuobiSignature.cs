using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Huobi.Client.Websocket.Utils;
using Microsoft.AspNetCore.Http;

namespace Huobi.Client.Websocket.Authentication
{
    public class HuobiSignature : IHuobiSignature
    {
        private const string METHOD = "GET";
        private const string NEW_LINE_CHAR = "\n";
        private const string ACCESS_KEY_NAME = "AccessKeyId";
        private const string TIMESTAMP_NAME = "Timestamp";

        private const string SIGNATURE_METHOD_NAME = "SignatureMethod";
        internal const string SIGNATURE_METHOD_VALUE = "HmacSHA256";

        private const string SIGNATURE_VERSION_NAME = "SignatureVersion";
        internal const string SIGNATURE_VERSION_VERSION = "2";

        public string Create(string accessKey, string secretKey, string host, string uri, DateTimeOffset timestamp)
        {
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            using var hmacSha256 = new HMACSHA256(secretKeyBytes);

            var requestString = CreateRequestStringFutures(accessKey, host, uri, timestamp);
            var requestStringBytes = Encoding.UTF8.GetBytes(requestString);
            var requestStringHash = hmacSha256.ComputeHash(requestStringBytes);

            var signature = Convert.ToBase64String(requestStringHash);
            return signature;
        }

        private static string CreateRequestString(string accessKey, string host, string uri, DateTimeOffset timestamp)
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

        private static string CreateRequestStringFutures(string accessKey, string baseurl, string path, DateTimeOffset timestamp)
        {
            var requestStringBuilder = new StringBuilder(METHOD);
            requestStringBuilder.Append(NEW_LINE_CHAR);
            requestStringBuilder.Append(baseurl);
            requestStringBuilder.Append(NEW_LINE_CHAR);
            requestStringBuilder.Append(path);
            requestStringBuilder.Append(NEW_LINE_CHAR);

            var urlParams = CreateQueryString(accessKey, timestamp);
            requestStringBuilder.Append(urlParams);

            var requestString = requestStringBuilder.ToString();
            // Console.WriteLine($"requestString: {requestString}");
            return requestString;
        }

        private static string CreateQueryString(string accessKey, DateTimeOffset timestamp)
        {
            var urlParamsDict = new Dictionary<string, string>
            {
                { ACCESS_KEY_NAME, accessKey },
                { SIGNATURE_METHOD_NAME, SIGNATURE_METHOD_VALUE },
                { SIGNATURE_VERSION_NAME, SIGNATURE_VERSION_VERSION },
                { TIMESTAMP_NAME, timestamp.ToHuobiUtcString() }
            };

            var urlParams = QueryString.Create(urlParamsDict);
            return urlParams.Value.Substring(1); // removes initial question mark
        }
    }
}