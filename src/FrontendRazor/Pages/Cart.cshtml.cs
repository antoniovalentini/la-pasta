using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaPasta.FrontendRazor.Pages;

public class CartModel : PageModel
{
    private readonly ILogger<CartModel> _logger;

    public CartModel(ILogger<CartModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
