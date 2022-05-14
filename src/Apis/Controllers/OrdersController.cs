using LaPasta.Apis.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaPasta.Apis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly ApiDbContext _dbContext;

    public OrdersController(ILogger<OrdersController> logger, ApiDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        _logger.LogInformation("Start {Method}", nameof(GetOrders));
        var orders = await _dbContext.Orders.Where(o => o.UserId == TheUser.UserId).Include(o => o.Items).ToListAsync();
        _logger.LogInformation("End {Method}", nameof(GetOrders));
        return orders.Select(o => new OrderDto(o.OrderId, o.Total, o.Status.ToString(),
            o.Items.Select(i => new OrderItemDto(i.ProductId, i.Description, i.Quantity, i.ActualProductPrice)).ToList()));
    }

    [HttpPost]
    public async Task<ActionResult<PostOrdersResponseDto>> PostOrders(PostOrdersRequestDto requestDto)
    {
        if (requestDto.Products is not { Count: > 0})
        {
            return BadRequest("Order products cannot be empty.");
        }

        var orderId = Guid.NewGuid().ToString();
        var items = requestDto.Products
            .Select(p => new OrderItem(p.Id, orderId, p.Quantity, _dbContext.Products.Single(p1 => p1.Id == p.Id).Price, _dbContext.Products.Single(p1 => p1.Id == p.Id).Description))
            .ToList();
        var order = new Order(
            orderId,
            TheUser.UserId,
            items,
            items.Sum(i => i.Quantity * long.Parse(i.ActualProductPrice)).ToString(),
            OrderStatus.InProgress);

        var result = await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return new PostOrdersResponseDto(result.Entity.OrderId);
    }
}

public record OrderDto(string OrderId, string Total, string Status, List<OrderItemDto>? Products);
public record OrderItemDto(string ProductId, string Description, int Quantity, string ActualProductPrice);
public record ProductDto(string Id, int Quantity);
public record PostOrdersRequestDto(List<ProductDto>? Products);
public record PostOrdersResponseDto(string OrderId);
