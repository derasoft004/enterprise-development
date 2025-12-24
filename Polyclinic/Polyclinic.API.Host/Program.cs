using Microsoft.EntityFrameworkCore;
using Polyclinic.Application.Interfaces;
using Polyclinic.Application.Services;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.PostgreSql;
using Polyclinic.Infrastructure.PostgreSql.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");

// Log for debugging
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"Connection string from config: {(string.IsNullOrEmpty(connectionString) ? "EMPTY OR NULL" : "FOUND")}");

if (string.IsNullOrEmpty(connectionString))
{
    // Fallback connection string
    connectionString = "Host=localhost;Port=5432;Database=polyclinic;Username=zerder;";
    Console.WriteLine($"Using fallback connection string: {connectionString}");
}

// Register DbContext
builder.Services.AddDbContext<PolyclinicDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register repositories
builder.Services.AddScoped<IRepository<Patient, int>, PostgresPatientRepository>();
builder.Services.AddScoped<IRepository<Doctor, int>, PostgresDoctorRepository>();
builder.Services.AddScoped<IRepository<Appointment, int>, PostgresAppointmentRepository>();
builder.Services.AddScoped<IRepository<Specialization, int>, PostgresSpecializationRepository>();

// Register services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// Register seeder
builder.Services.AddScoped<PostgresSeeder>();

var app = builder.Build();

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<PolyclinicDbContext>();
        Console.WriteLine("Applying migrations...");
        
        // Check if database exists
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database connected successfully.");
            dbContext.Database.Migrate();
            
            var seeder = scope.ServiceProvider.GetRequiredService<PostgresSeeder>();
            Console.WriteLine("Seeding data...");
            seeder.Seed();
        }
        else
        {
            Console.WriteLine("Cannot connect to database. Creating database...");
            dbContext.Database.EnsureCreated();
            
            var seeder = scope.ServiceProvider.GetRequiredService<PostgresSeeder>();
            Console.WriteLine("Seeding data...");
            seeder.Seed();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during migration/seeding: {ex.Message}");
        Console.WriteLine($"Full error: {ex}");
        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Polyclinic API v1");
    c.RoutePrefix = "swagger"; 
    c.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }