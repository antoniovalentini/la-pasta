using LaPasta.Api.Dtos;

namespace LaPasta.Api.Persistence;

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

public static class DtoExtensions
{
    public static FullProductDto ToDto(this Product entity) =>
        new(entity.Id, entity.Name, entity.Description, entity.Price);
}
