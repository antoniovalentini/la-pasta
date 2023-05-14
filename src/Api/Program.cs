using LaPasta.Api.Persistence;
using LaPasta.Api.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddDbContext<ApiDbContext>(options =>
    {
        var connectionString = builder.Configuration["DbConnectionString"];
        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        options.UseSqlite(connectionString);
    })
    .AddSingleton<IUserIdentityProvider, TestUserIdentityProvider>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.UseSwagger()
    .UseSwaggerUI()
    .UseAuthorization();

app.MapControllers();

app.Run();

// Necessary for internal visibility in integration tests
public partial class Program { }
