using System;
using System.Linq;
using LaPasta.Apis.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LaPasta.Apis.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.Single(d => d.ServiceType == typeof(ApiDbContext));

            services.Remove(descriptor);

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();

            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApiDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<Program>>>();

            db.Database.EnsureCreated();

            try
            {
                DbSeed.PopulateDb(db).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }
        });
    }
}
