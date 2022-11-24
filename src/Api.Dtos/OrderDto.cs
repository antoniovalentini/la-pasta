namespace LaPasta.Api.Dtos;

public record OrderDto(string OrderId, string Total, string Status, DateTime PurchaseDate, List<OrderItemDto>? Products);
public record OrderItemDto(string ProductId, string Description, int Quantity, string ActualProductPrice);
