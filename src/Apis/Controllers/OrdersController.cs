using LaPasta.Apis.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaPasta.Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly ApiDbContext _dbContext;

    public OrdersController(ILogger<OrdersController> logger, ApiDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("GetOrders")]
    public async Task<IEnumerable<Order>> Get()
    {
        _logger.LogInformation("Start GetOrder");
        var orders = await _dbContext.Orders.Where(o => o.UserId == TheUser.UserId).Include(o => o.Items).ToListAsync();
        _logger.LogInformation("End GetOrder");
        return orders;
    }
}
