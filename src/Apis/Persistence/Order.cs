using System.ComponentModel.DataAnnotations.Schema;

namespace LaPasta.Apis.Persistence;

public class Order
{
    // Used by EF
    public Order() { }

    public Order(string orderId, string userId, List<OrderItem> items, string total, OrderStatus status)
    {
        OrderId = orderId;
        UserId = userId;
        Items = items;
        Total = total;
        Status = status;
    }

    public string OrderId { get; init; } = null!;
    public string UserId { get; init; } = null!;
    public List<OrderItem> Items { get; init; } = null!;
    public string Total { get; init; } = null!;
    public OrderStatus Status { get; init; }
}

public class OrderItem
{
    public OrderItem(string productId, string orderId, int quantity, string actualProductPrice)
    {
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        ActualProductPrice = actualProductPrice;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; } = null!;
    public string ProductId { get; init; }
    public int Quantity { get; init; }
    public string ActualProductPrice { get; init; }
    public string OrderId { get; init; }
}

public enum OrderStatus
{
    InProgress,
    Shipped,
    Blocked,
}
