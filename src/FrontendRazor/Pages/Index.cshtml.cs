using System.Text.Json;
using LaPasta.Apis.Controllers;
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
        var httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:5008/")};
        var response = await httpClient.GetAsync("api/Products");
        if (!response.IsSuccessStatusCode) return;
        var raw = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<FullProductDto>>(raw, new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        if (products is null)
        {
            var ex = new Exception("Unable to deserialize products list.");
            _logger.LogError(ex, ex.Message);
            throw ex;
        }
        Products = products;
    }
}
