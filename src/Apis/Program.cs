using LaPasta.Apis.Persistence;
using LaPasta.Apis.Users;
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// else
// {
    // disable until we figure out how to trust certificates in docker
    // app.UseHttpsRedirection();
// }

app.UseAuthorization();

app.MapControllers();

app.Run();

// Necessary for internal visibility in integration tests
public partial class Program
{

}
