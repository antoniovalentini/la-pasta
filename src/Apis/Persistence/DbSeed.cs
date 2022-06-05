namespace LaPasta.Apis.Persistence;

public static class DbSeed
{
    public static readonly Product[] Products = {
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

    public static async Task PopulateDb(ApiDbContext dbContext)
    {
        // Seed products
        await dbContext.Products.AddRangeAsync(Products);

        // Seed orders
        var orderId = Guid.NewGuid().ToString();
        var items = new List<OrderItem>
        {
            new(Products[0].Id, orderId, Random.Shared.Next(0, 10), Products[0].Price, Products[0].Description),
            new(Products[1].Id, orderId, Random.Shared.Next(0, 10), Products[1].Price, Products[1].Description),
            new(Products[2].Id, orderId, Random.Shared.Next(0, 10), Products[2].Price, Products[2].Description),
        };

        var order = new Order(orderId, TheUser.UserId, items, "180,11", OrderStatus.InProgress, DateTime.UtcNow.AddDays(-300));
        await dbContext.Orders.AddAsync(order);

        orderId = Guid.NewGuid().ToString();
        items = new List<OrderItem>
        {
            new(Products[3].Id, orderId, Random.Shared.Next(0, 10), Products[3].Price, Products[3].Description),
            new(Products[4].Id, orderId, Random.Shared.Next(0, 10), Products[4].Price, Products[4].Description),
            new(Products[5].Id, orderId, Random.Shared.Next(0, 10), Products[5].Price, Products[5].Description),
        };
        order = new Order(orderId, TheUser.UserId, items, "99,99", OrderStatus.Blocked, DateTime.UtcNow.AddDays(-800));
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }
}
