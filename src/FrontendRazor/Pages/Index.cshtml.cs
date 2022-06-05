using System.Text.Json;
using LaPasta.Apis.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.FrontendRazor.Pages;

public class IndexModel : PageModel
{
    public List<FullProductDto> Products { get; set; }

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        Products = new List<FullProductDto>();
    }

    public async Task OnGet()
    {
        Products = await FetchProducts();
    }

    public async Task OnPost(string id)
    {
        Products = await FetchProducts();
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            throw new Exception($"Invalid product id: '{id}'");
        }
        var cart = HttpContext.Session.GetObjectFromJson<Cart>(SessionHelper.CartSessionId) ?? new Cart();
        cart.Items.Add(product);
        HttpContext.Session.SetObjectAsJson(SessionHelper.CartSessionId, cart);
    }

    private async Task<List<FullProductDto>> FetchProducts()
    {
        var httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:5008/")};
        var response = await httpClient.GetAsync("api/Products");
        if (!response.IsSuccessStatusCode)
        {
            var ex1 = new Exception("Unable to fetch products.");
            _logger.LogError(ex1, ex1.Message);
            throw ex1;
        }
        var raw = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<FullProductDto>>(raw, new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        if (products is not null)
        {
            return products;
        }

        var ex2 = new Exception("Unable to deserialize products list.");
        _logger.LogError(ex2, ex2.Message);
        throw ex2;
    }
}
