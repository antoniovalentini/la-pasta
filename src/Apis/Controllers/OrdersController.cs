using Microsoft.AspNetCore.Mvc;

namespace LaPasta.Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly string[] Pasta = {
        "Fusilli", "Spaghetti", "Rigatoni", "Penne", "Farfalle", "Tortiglioni", "Paccheri", "Orecchiette", "Trofie"
    };

    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetOrders")]
    public IEnumerable<Order> Get()
    {
        _logger.LogInformation("Start GetOrder");
        var items = Enumerable.Range(1, Random.Shared.Next(2, 4))
            .Select(_ => new OrderItem(Pasta[Random.Shared.Next(0, Pasta.Length)], Random.Shared.Next(1, 10)));
        var orders = Enumerable.Range(1, Random.Shared.Next(2, 5))
            .Select(index => new Order(index.ToString(), TheUser.UserId, items, (OrderStatus)Random.Shared.Next(0, 3)))
            .ToArray();
        _logger.LogInformation("End GetOrder");
        return orders;
    }
}
