namespace Huobi.Client.Websocket.Messages.MarketData.Values;

public class BookLevel
{
    public BookLevel(OrderBookSide side, decimal price, decimal size)
    {
        Side = side;
        Price = price;
        Size = size;
    }

    public OrderBookSide Side { get; }

    public decimal Price { get; }

    public decimal Size { get; }
}