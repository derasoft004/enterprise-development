using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.PostgreSQL.Configurations;
using Polyclinic.Infrastructure.PostgreSQL.Repository;

namespace Polyclinic.Infrastructure.PostgreSQL.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add PostgreSQL infrastructure to the service collection
    /// </summary>
    public static IServiceCollection AddPostgreSqlInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Get PostgresSettings
        var postgresSettings = new PostgresSettings();
        configuration.GetSection(PostgresSettings.SectionName).Bind(postgresSettings);
        
        if (string.IsNullOrEmpty(postgresSettings.ConnectionString))
        {
            throw new InvalidOperationException(
                "PostgreSQL connection string is not configured");
        }
        
        // Register DbContext
        services.AddDbContext<PolyclinicDbContext>(options =>
        {
            options.UseNpgsql(postgresSettings.ConnectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(
                    typeof(PolyclinicDbContext).Assembly.FullName);
            });
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });
        
        // Register repositories
        services.AddScoped<IRepository<Patient, int>, PostgresPatientRepository>();
        services.AddScoped<IRepository<Doctor, int>, PostgresDoctorRepository>();
        services.AddScoped<IRepository<Appointment, int>, PostgresAppointmentRepository>();
        
        // Register seeder
        services.AddScoped<PostgresSeeder>();
        
        return services;
    }
}