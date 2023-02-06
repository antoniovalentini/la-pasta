namespace Backoffice.Razor.Extensions;

public static class OrderStatusMapperExtensions
{
    public static string GetStatusBadge(this string status)
    {
        return status.ToLower() switch
        {
            "inprogress" => "bg-warning text-dark",
            "shipped" => "bg-info text-dark",
            "delivered" => "bg-success",
            "blocked" => "bg-danger",
            _ => "bg-secondary"
        };
    }
}
