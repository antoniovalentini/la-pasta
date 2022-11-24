namespace LaPasta.Apis.Persistence;

public class Product
{
    // Used by EF
    public Product() { }

    public Product(string id, string name, string description, string price)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
    }

    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Price { get; set; } = null!;
}
