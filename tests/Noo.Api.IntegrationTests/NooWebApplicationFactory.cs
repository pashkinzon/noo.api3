using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.IntegrationTests;

public class NooWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace the production database with an in-memory one
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<NooDbContext>)
            );

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<NooDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            // Seed initial data
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<NooDbContext>();
            db.Database.EnsureCreated();

            AddTestUsers(db);
            // Add any initial data you want to seed here

            db.SaveChanges();
        });
    }

    public void AddTestUsers(NooDbContext context)
    {
        // TODO: Add test users, at least one for each role
    }
}