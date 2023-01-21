using LaPasta.Api.Dtos;
using LaPasta.Api.Persistence;
using LaPasta.Api.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaPasta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ApiDbContext _dbContext;
    private readonly IUserIdentityProvider _userIdentityProvider;

    public OrdersController(ApiDbContext dbContext, IUserIdentityProvider userIdentityProvider)
    {
        _dbContext = dbContext;
        _userIdentityProvider = userIdentityProvider;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        var userId = await _userIdentityProvider.GetCurrentUserIdAsync();

        var orders = await _dbContext.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .ToListAsync();

        return orders.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<PostOrdersResponseDto>> PostOrders(PostOrdersRequestDto requestDto)
    {
        if (requestDto.Products is not { Count: > 0})
        {
            return BadRequest("Order products cannot be empty.");
        }

        var userId = await _userIdentityProvider.GetCurrentUserIdAsync();
        var orderId = Guid.NewGuid().ToString();

        var items = requestDto.Products
            .Select(p =>
            {
                var actualProductPrice = _dbContext.Products.Single(p1 => p1.Id == p.Id).Price;
                var description = _dbContext.Products.Single(p1 => p1.Id == p.Id).Description;
                return new OrderItem(p.Id, orderId, p.Quantity, actualProductPrice, description);
            })
            .ToList();

        var total = items.Sum(i => i.Quantity * long.Parse(i.ActualProductPrice)).ToString();
        var order = new Order(orderId, userId, items, total, OrderStatus.InProgress, DateTime.UtcNow);

        var result = await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return new PostOrdersResponseDto(result.Entity.OrderId);
    }
}
