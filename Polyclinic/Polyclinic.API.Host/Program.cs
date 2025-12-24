using Microsoft.EntityFrameworkCore;
using Polyclinic.Application.Interfaces;
using Polyclinic.Application.Services;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.PostgreSql;
using Polyclinic.Infrastructure.PostgreSql.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
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

if (!builder.Environment.IsEnvironment("Testing"))
{
    var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");

    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = "Host=localhost;Port=5432;Database=polyclinic;Username=zerder;";
    }

    builder.Services.AddDbContext<PolyclinicDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // try inmemory 
    builder.Services.AddDbContext<PolyclinicDbContext>(options =>
        options.UseInMemoryDatabase("Polyclinic_TestDb"));
}

// Repositories
builder.Services.AddScoped<IRepository<Patient, int>, PostgresPatientRepository>();
builder.Services.AddScoped<IRepository<Doctor, int>, PostgresDoctorRepository>();
builder.Services.AddScoped<IRepository<Appointment, int>, PostgresAppointmentRepository>();
builder.Services.AddScoped<IRepository<Specialization, int>, PostgresSpecializationRepository>();

// Services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// Seeder
builder.Services.AddScoped<PostgresSeeder>();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<PolyclinicDbContext>();

    if (dbContext.Database.CanConnect())
    {
        dbContext.Database.Migrate();
    }
    else
    {
        dbContext.Database.EnsureCreated();
    }

    var seeder = scope.ServiceProvider.GetRequiredService<PostgresSeeder>();
    seeder.Seed();
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