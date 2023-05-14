var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var backendUrl = builder.Configuration["BackendUrl"];
ArgumentException.ThrowIfNullOrEmpty(backendUrl);

builder.Services.AddHttpClient("backend", client =>
{
    client.BaseAddress = new Uri(backendUrl);
});

var app = builder.Build();

app.UseStaticFiles()
    .UseRouting();

app.MapRazorPages();

app.Run();
