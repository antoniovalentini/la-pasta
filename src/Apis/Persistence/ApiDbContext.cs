using Microsoft.EntityFrameworkCore;

namespace LaPasta.Apis.Persistence;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
        // new DbSeed(this).PopulateDb().GetAwaiter().GetResult();
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();
}
