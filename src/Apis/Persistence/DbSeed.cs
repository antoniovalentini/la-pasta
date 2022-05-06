namespace LaPasta.Apis.Persistence;

public class DbSeed
{
    private static readonly string[] Pasta = {
        "Fusilli", "Spaghetti", "Rigatoni", "Penne", "Farfalle", "Tortiglioni", "Paccheri", "Orecchiette", "Trofie"
    };

    public async Task PopulateDb(ApiDbContext dbContext)
    {
        // Seed products
        var products = Pasta.Select(p => new Product(Guid.NewGuid().ToString(), p)).ToList();
        await dbContext.Products.AddRangeAsync(products);

        // Seed orders
        var items = new List<OrderItem>
        {
            new(products[0].Id, Random.Shared.Next(0, 10)),
            new(products[1].Id, Random.Shared.Next(0, 10)),
            new(products[2].Id, Random.Shared.Next(0, 10)),
        };
        var order = new Order(Guid.NewGuid().ToString(), TheUser.UserId, items, OrderStatus.InProgress);
        await dbContext.Orders.AddAsync(order);
        items = new List<OrderItem>
        {
            new(products[3].Id, Random.Shared.Next(0, 10)),
            new(products[4].Id, Random.Shared.Next(0, 10)),
            new(products[5].Id, Random.Shared.Next(0, 10)),
        };
        order = new Order(Guid.NewGuid().ToString(), TheUser.UserId, items, OrderStatus.Blocked);
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }
}
