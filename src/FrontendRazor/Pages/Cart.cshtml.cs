using LaPasta.Apis.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.FrontendRazor.Pages;

public class CartModel : PageModel
{
    public Cart Cart { get; set; } = new();
    public decimal Total { get; set; } = 0;

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
}

public class Cart
{
    public List<CartItem> Items { get; init; } = new();
}

public record CartItem(string ItemId, FullProductDto Product);
