using LaPasta.Apis.Persistence;

namespace LaPasta.Apis.Dtos;

public record FullProductDto(string Id, string Name, string Description, string Price)
{
    public static FullProductDto FromEntity(Product entity)
        => new FullProductDto(entity.Id, entity.Name, entity.Description, entity.Price);
}
