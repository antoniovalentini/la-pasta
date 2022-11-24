var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSession();

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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // app.UseHsts();
    // app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseSession();

app.Run();
