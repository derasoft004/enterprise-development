using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSql;

public class PolyclinicDbContext(
    DbContextOptions<PolyclinicDbContext> options
) : DbContext(options)
{
    /// <summary>
    /// Patients table
    /// </summary>
    public DbSet<Patient> Patients { get; set; }
    
    /// <summary>
    /// Doctors table
    /// </summary>
    public DbSet<Doctor> Doctors { get; set; }
    
    /// <summary>
    /// Appointments table
    /// </summary>
    public DbSet<Appointment> Appointments { get; set; }
    
    /// <summary>
    /// Specializations table
    /// </summary>
    public DbSet<Specialization> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PolyclinicDbContext).Assembly);
    }
}