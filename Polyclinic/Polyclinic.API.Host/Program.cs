using Polyclinic.Application.Interfaces;
using Polyclinic.Application.Services;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.InMemory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repositories
builder.Services.AddScoped<IRepository<Patient, int>, InMemoryPatientRepository>();
builder.Services.AddScoped<IRepository<Doctor, int>, InMemoryDoctorRepository>();
builder.Services.AddScoped<IRepository<Appointment, int>, InMemoryAppointmentRepository>();
builder.Services.AddScoped<IRepository<Specialization, int>, InMemorySpecializationRepository>();

// Register Services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();