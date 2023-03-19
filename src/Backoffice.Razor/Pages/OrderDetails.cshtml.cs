using System.Text.Json;
using LaPasta.Api.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Backoffice.Razor.Pages;

public class OrderDetails : PageModel
{
    private readonly HttpClient _httpClient;

    public OrderDto? Order { get; set; }

    public OrderDetails(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("backend");
    }

    public async Task OnGet(string id)
    {
        var response = await _httpClient.GetAsync($"api/Orders/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var ex1 = new Exception($"Unable to fetch order '{id}' because API returned '{response.StatusCode}'");
            throw ex1;
        }
        var raw = await response.Content.ReadAsStringAsync();
        var order = JsonSerializer.Deserialize<OrderDto>(raw, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        if (order is not null)
        {
            Order = order;
        }
    }
}
