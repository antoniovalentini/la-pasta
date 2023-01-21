using LaPasta.Api.Dtos;

namespace LaPasta.Api.Persistence;

public class Order
{
    // Used by EF
    public Order() { }

    public Order(string orderId, string userId, List<OrderItem> items, string total, OrderStatus status, DateTime purchaseDate)
    {
        OrderId = orderId;
        UserId = userId;
        Items = items;
        Total = total;
        Status = status;
        PurchaseDate = purchaseDate;
    }

    public string OrderId { get; init; } = null!;
    public string UserId { get; init; } = null!;
    public List<OrderItem> Items { get; init; } = null!;
    public string Total { get; init; } = null!;
    public OrderStatus Status { get; init; }
    public DateTime PurchaseDate { get; init; }
}

public static class OrderExtensions
{
    public static OrderDto ToDto(this Order o) =>
        new(o.OrderId, o.Total, o.Status.ToString(), o.PurchaseDate, o.Items.ToDto());

    public static IEnumerable<OrderDto> ToDto(this IEnumerable<Order> orders) => orders.Select(o => o.ToDto());
}
