using LaPasta.Apis.Dtos;
using LaPasta.Apis.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaPasta.Apis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApiDbContext _dbContext;

    public ProductsController(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<FullProductDto>> GetProducts()
    {
        var orders = await _dbContext.Products.ToListAsync();

        return orders.Select(FullProductDto.FromEntity);
    }
}
