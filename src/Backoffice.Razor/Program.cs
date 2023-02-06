var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var backendUrl = builder.Configuration["BackendUrl"];
if (string.IsNullOrWhiteSpace(backendUrl))
{
    throw new Exception("BackendUrl is not defined.");
}

builder.Services.AddHttpClient("backend", client =>
{
    client.BaseAddress = new Uri(backendUrl);
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
