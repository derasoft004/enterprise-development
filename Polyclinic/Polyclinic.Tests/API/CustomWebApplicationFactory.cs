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

            // try inmemory database
            services.AddDbContext<PolyclinicDbContext>(options =>
                options.UseInMemoryDatabase("Polyclinic_TestDb"));
        });
    }
}