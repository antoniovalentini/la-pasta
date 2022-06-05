using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using LaPasta.Apis.Dtos;
using LaPasta.Apis.Persistence;
using Xunit;

namespace LaPasta.Apis.IntegrationTests.Controllers;

public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public ProductsControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Products_Should_Return_Ok()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/products");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue(content);

        var products = JsonSerializer.Deserialize<IEnumerable<FullProductDto>>(content);
        products.Should().HaveCount(DbSeed.Products.Length);
    }
}
