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
    public async Task<IEnumerable<Order>> GetOrders()
    {
        _logger.LogInformation("Start {Method}", nameof(GetOrders));
        var orders = await _dbContext.Orders.Where(o => o.UserId == TheUser.UserId).Include(o => o.Items).ToListAsync();
        _logger.LogInformation("End {Method}", nameof(GetOrders));
        return orders;
    }

    [HttpPost]
    public async Task<ActionResult<PostOrdersResponse>> PostOrders(PostOrdersRequest request)
    {
        if (request.Order is null)
        {
            return BadRequest("Order cannot be null.");
        }

        if (request.Order.Products is not { Count: > 0})
        {
            return BadRequest("Order products cannot be empty.");
        }

        var orderId = Guid.NewGuid().ToString();
        var items = request.Order.Products
            .Select(p => new OrderItem(p.Id, orderId, p.Quantity, _dbContext.Products.Single(p1 => p1.Id == p.Id).Price))
            .ToList();
        var order = new Order(
            orderId,
            TheUser.UserId,
            items,
            items.Sum(i => i.Quantity * long.Parse(i.ActualProductPrice)).ToString(),
            OrderStatus.InProgress);

        var result = await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return new PostOrdersResponse(result.Entity);
    }
}

public record OrderDto(List<ProductDto>? Products);
public record ProductDto(string Id, int Quantity);
public record PostOrdersRequest(OrderDto? Order);
public record PostOrdersResponse(Order Order);
