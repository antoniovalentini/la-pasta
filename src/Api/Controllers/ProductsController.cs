using LaPasta.Api.Dtos;
using LaPasta.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaPasta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApiDbContext _dbContext;

    public ProductsController(ApiDbContext dbContext) => _dbContext = dbContext;

    [HttpGet]
    public async Task<IEnumerable<FullProductDto>> GetProducts() =>
        await _dbContext.Products.Select(x => x.ToDto()).ToListAsync();
}
