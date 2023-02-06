using System.ComponentModel.DataAnnotations.Schema;
using LaPasta.Api.Dtos;

namespace LaPasta.Api.Persistence;

public class OrderItem
{
    public OrderItem(string productId, string orderId, int quantity, string actualProductPrice, string name, string description)
    {
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        ActualProductPrice = actualProductPrice;
        Name = name;
        Description = description;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; } = null!;
    public string ProductId { get; init; }
    public string OrderId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public string ActualProductPrice { get; init; }
}

public static class OrderItemExtensions
{
    public static OrderItemDto ToDto(this OrderItem i) =>
        new(i.ProductId, i.Name, i.Description, i.Quantity, i.ActualProductPrice);

    public static List<OrderItemDto> ToDto(this IEnumerable<OrderItem> items) =>
        items.Select(i => i.ToDto()).ToList();
}
