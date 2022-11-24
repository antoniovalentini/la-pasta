using System.Text.Json;
using LaPasta.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.Frontend.Razor.Pages;

public class IndexModel : PageModel
{
    public List<FullProductDto> Products { get; set; }

    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;

    [TempData]
    public string? Message { get; set; }

    public bool HasMessage => !string.IsNullOrEmpty(Message);

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory factory)
    {
        _logger = logger;
        _httpClient = factory.CreateClient("backend");
        Products = new List<FullProductDto>();
    }

    public async Task OnGet()
    {
        Products = await FetchProducts();
    }

    public async Task<IActionResult> OnPost(string id)
    {
        Products = await FetchProducts();
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            throw new Exception($"Invalid product id: '{id}'");
        }
        var cart = HttpContext.Session.GetObjectFromJson<Cart>(SessionHelper.CartSessionId) ?? new Cart();
        cart.Items.Add(new CartItem(Guid.NewGuid().ToString(),product));
        HttpContext.Session.SetObjectAsJson(SessionHelper.CartSessionId, cart);
        Message = $"{product.Name} was added to your cart";
        return RedirectToPage();
    }

    private async Task<List<FullProductDto>> FetchProducts()
    {
        var response = await _httpClient.GetAsync("api/Products");
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
