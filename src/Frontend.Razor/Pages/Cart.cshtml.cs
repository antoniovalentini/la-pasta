using System.Net.Mime;
using System.Text;
using System.Text.Json;
using LaPasta.Apis.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.Frontend.Razor.Pages;

public class CartModel : PageModel
{
    public Cart Cart { get; set; } = new();
    public decimal Total { get; set; } = 0;

    private readonly HttpClient _httpClient;

    [TempData]
    public string? Message { get; set; }

    public CartModel(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("backend");
    }

    public void OnGet()
    {
        Cart = HttpContext.Session.GetObjectFromJson<Cart>(SessionHelper.CartSessionId) ?? new Cart();
        Total = Cart.Items.Sum(i => decimal.Parse(i.Product.Price));
    }

    public IActionResult OnPostDelete(string id)
    {
        Cart = HttpContext.Session.GetObjectFromJson<Cart>(SessionHelper.CartSessionId) ?? new Cart();
        if (Cart.Items is not {Count: > 0}) return RedirectToPage();
        Cart.Items.RemoveAll(i => i.ItemId == id);
        HttpContext.Session.SetObjectAsJson(SessionHelper.CartSessionId, Cart);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPost()
    {
        Cart = HttpContext.Session.GetObjectFromJson<Cart>(SessionHelper.CartSessionId) ?? new Cart();
        if (Cart.Items is not {Count: > 0}) return RedirectToPage();

        var dict = new Dictionary<string, int>();
        foreach (var cartItem in Cart.Items)
        {
            if (dict.ContainsKey(cartItem.Product.Id))
            {
                dict[cartItem.Product.Id]++;
            }
            else
            {
                dict.Add(cartItem.Product.Id, 1);
            }
        }

        // Send order to backend
        var items = dict.Select(i => new BasicProductDto(i.Key, i.Value)).ToList();
        var order = new PostOrdersRequestDto(items);
        var serializedOrders = JsonSerializer.Serialize(order);
        var httpContent = new StringContent(serializedOrders, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("api/Orders", httpContent);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            var ex1 = new Exception($"Unable to send order: {content}");
            throw ex1;
        }

        Cart.Items.Clear();
        HttpContext.Session.SetObjectAsJson(SessionHelper.CartSessionId, Cart);
        Message = "Order has been sent!";
        return RedirectToPage("Orders");
    }
}

public class Cart
{
    public List<CartItem> Items { get; init; } = new();
}

public record CartItem(string ItemId, FullProductDto Product);
