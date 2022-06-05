using LaPasta.Apis.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.FrontendRazor.Pages;

public class CartModel : PageModel
{
    private readonly ILogger<CartModel> _logger;
    public Cart Cart { get; set; } = new Cart();

    public CartModel(ILogger<CartModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Cart = HttpContext.Session.GetObjectFromJson<Cart>(SessionHelper.CartSessionId) ?? new Cart();
    }

    public IActionResult OnPostDelete(string id)
    {
        Cart = HttpContext.Session.GetObjectFromJson<Cart>(SessionHelper.CartSessionId) ?? new Cart();
        if (Cart.Items is not {Count: > 0}) return RedirectToPage();
        Cart.Items.RemoveAll(i => i.Id == id);
        HttpContext.Session.SetObjectAsJson(SessionHelper.CartSessionId, Cart);

        return RedirectToPage();
    }
}

public class Cart
{
    public List<FullProductDto> Items { get; set; } = new();
}
