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
}
