using Microsoft.EntityFrameworkCore;

namespace LaPasta.Api.Persistence;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
        // DbSeed.PopulateDb(this).GetAwaiter().GetResult();
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();
}
