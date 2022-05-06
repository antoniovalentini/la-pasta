namespace LaPasta.Apis.Persistence;

public class Product
{
    // Used by EF
    public Product() { }

    public Product(string id, string description)
    {
        Id = id;
        Description = description;
    }

    public string Id { get; set; } = null!;
    public string Description { get; set; } = null!;
}
