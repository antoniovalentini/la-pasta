using System.Text.Json;

namespace LaPasta.FrontendRazor;

public static class SessionHelper
{
    public const string CartSessionId = "CART";

    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        var serialized = JsonSerializer.Serialize(value);
        session.SetString(key, serialized);
    }

    public static T? GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return string.IsNullOrWhiteSpace(value) ? default : JsonSerializer.Deserialize<T>(value);
    }
}
