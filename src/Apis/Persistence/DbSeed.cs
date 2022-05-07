namespace LaPasta.Apis.Persistence;

public class DbSeed
{
    private static readonly Product[] Products = {
        new("1", "Fusilli",     "Descrizione Fusilli",     "5"),
        new("2", "Spaghetti",   "Descrizione Spaghetti",   "6"),
        new("3", "Rigatoni",    "Descrizione Rigatoni",    "4"),
        new("4", "Penne",       "Descrizione Penne",       "5"),
        new("5", "Farfalle",    "Descrizione Farfalle",    "7"),
        new("6", "Tortiglioni", "Descrizione Tortiglioni", "8"),
        new("7", "Paccheri",    "Descrizione Paccheri",    "4"),
        new("8", "Orecchiette", "Descrizione Orecchiette", "3"),
        new("9", "Trofie",      "Descrizione Trofie",      "4"),
    };

    public async Task PopulateDb(ApiDbContext dbContext)
    {
        // Seed products
        await dbContext.Products.AddRangeAsync(Products);

        // Seed orders
        var items = new List<OrderItem>
        {
            new(Products[0].Id, Random.Shared.Next(0, 10), Products[0].Price),
            new(Products[1].Id, Random.Shared.Next(0, 10), Products[1].Price),
            new(Products[2].Id, Random.Shared.Next(0, 10), Products[2].Price),
        };

        var order = new Order(Guid.NewGuid().ToString(), TheUser.UserId, items, "180,11", OrderStatus.InProgress);
        await dbContext.Orders.AddAsync(order);
        items = new List<OrderItem>
        {
            new(Products[3].Id, Random.Shared.Next(0, 10), Products[3].Price),
            new(Products[4].Id, Random.Shared.Next(0, 10), Products[4].Price),
            new(Products[5].Id, Random.Shared.Next(0, 10), Products[5].Price),
        };
        order = new Order(Guid.NewGuid().ToString(), TheUser.UserId, items, "99,99", OrderStatus.Blocked);
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }
}
