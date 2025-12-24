using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polyclinic.Infrastructure.PostgreSql;

namespace Polyclinic.Tests.API;

/// <summary>
/// Custom web application factory for integration tests
/// </summary>
public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<PolyclinicDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<PolyclinicDbContext>(options =>
                options.UseNpgsql(
                    "Host=localhost;Port=5432;Database=polyclinic_test;Username=zerder"));

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PolyclinicDbContext>();

            context.Database.EnsureDeleted();
            context.Database.Migrate();
        });
    }
}