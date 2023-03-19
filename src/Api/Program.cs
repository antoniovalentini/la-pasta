using LaPasta.Api.Persistence;
using LaPasta.Api.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlite("Data Source=Database.db"));

builder.Services.AddSingleton<IUserIdentityProvider, TestUserIdentityProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Necessary for internal visibility in integration tests
public partial class Program { }
