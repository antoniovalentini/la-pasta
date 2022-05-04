namespace LaPasta.Apis;

public record Order(string OrderId, string UserId, IEnumerable<OrderItem> Items, OrderStatus Status);

public record OrderItem(string Id, int Quantity);

public enum OrderStatus
{
    InProgress,
    Shipped,
    Blocked,
}
