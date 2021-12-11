namespace Huobi.Client.Websocket.Messages.Account.Values;

public enum OrderType
{
    Unknown,
    BuyMarket,
    SellMarket,
    BuyLimit,
    SellLimit,
    BuyLimitMaker,
    SellLimitMaker,
    BuyIoc,
    SellIoc,
    BuyLimitFok,
    SellLimitFok,
    BuyStopLimit,
    SellStopLimit,
    BuyStopLimitFok,
    SellStopLimitFok
}