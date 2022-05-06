namespace LaPasta.Apis.Persistence;

public class Order
{
    // Used by EF
    public Order() { }

    public Order(string orderId, string userId, List<OrderItem> items, OrderStatus status)
    {
        OrderId = orderId;
        UserId = userId;
        Items = items;
        Status = status;
    }

    public string OrderId { get; init; } = null!;
    public string UserId { get; init; } = null!;
    public List<OrderItem> Items { get; init; } = null!;
    public OrderStatus Status { get; init; }
}

public record OrderItem(string Id, int Quantity);

public enum OrderStatus
{
    InProgress,
    Shipped,
    Blocked,
}
