using System.Text.Json;
using LaPasta.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.Frontend.Razor.Pages;

public class Orders : PageModel
{
    private readonly HttpClient _httpClient;

    public OrderDto[] OrdersList { get; set; } = Array.Empty<OrderDto>();

    [TempData]
    public string? Message { get; set; }
    public bool HasMessage => !string.IsNullOrEmpty(Message);

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
            OrdersList = orders.OrderByDescending(o => o.PurchaseDate).ToArray();
        }
    }
}
