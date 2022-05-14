using LaPasta.Apis.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaPasta.Apis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly ApiDbContext _dbContext;

    public ProductsController(ILogger<ProductsController> logger, ApiDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<FullProductDto>> GetProducts()
    {
        _logger.LogInformation("Start {Method}", nameof(GetProducts));
        var orders = await _dbContext.Products.ToListAsync();
        _logger.LogInformation("End {Method}", nameof(GetProducts));
        return orders.Select(FullProductDto.FromEntity);
    }
}

public record FullProductDto(string Id, string Name, string Description, string Price)
{
    public static FullProductDto FromEntity(Product entity)
        => new FullProductDto(entity.Id, entity.Name, entity.Description, entity.Price);
}
