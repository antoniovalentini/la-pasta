using System.Text.Json;
using LaPasta.Apis.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.FrontendRazor.Pages;

public class Orders : PageModel
{
    private readonly HttpClient _httpClient;

    public OrderDto[] OrdersList { get; set; } = Array.Empty<OrderDto>();

    public Orders(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("backend");
    }

    public async Task OnGet()
    {
        var response = await _httpClient.GetAsync("api/Orders");
        if (!response.IsSuccessStatusCode)
        {
            var ex1 = new Exception("Unable to fetch orders.");
            throw ex1;
        }
        var raw = await response.Content.ReadAsStringAsync();
        var orders = JsonSerializer.Deserialize<OrderDto[]>(raw, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        if (orders is {Length: > 0})
        {
            OrdersList = orders;
        }
    }
}
