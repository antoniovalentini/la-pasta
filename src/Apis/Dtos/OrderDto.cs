namespace LaPasta.Apis.Dtos;

public record OrderDto(string OrderId, string Total, string Status, List<OrderItemDto>? Products);
public record OrderItemDto(string ProductId, string Description, int Quantity, string ActualProductPrice);
