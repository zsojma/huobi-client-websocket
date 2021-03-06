﻿using System;
using Huobi.Client.Websocket.Authentication;
using Xunit;

namespace Huobi.Client.Websocket.Tests
{
    public class HuobiAuthenticationTests
    {
        [Fact]
        public void CreateSignature()
        {
            // Arrange
            var authentication = new HuobiSignature();
            var timestamp = new DateTimeOffset(2021, 03, 30, 12, 13, 14, TimeSpan.Zero);

            // Act
            var signature = authentication.Create(
                "123",
                "456",
                "api.huobi.pro",
                "/ws/v2",
                timestamp);

            // Assert
            Assert.Contains("J7Q1UdY5zVyh2xjUbPK2+n0elG+cb2DAeTU+mK4MeuE=", signature);
        }
    }
}