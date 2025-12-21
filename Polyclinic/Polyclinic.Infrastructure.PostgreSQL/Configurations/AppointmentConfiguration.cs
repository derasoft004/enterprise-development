using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSQL.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");
        
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();
            
        builder.Property(a => a.AppointmentDateTime)
            .IsRequired();
            
        builder.Property(a => a.RoomNumber);
            
        builder.Property(a => a.RepeatAppointment)
            .HasDefaultValue(false);
            
        // Relationships - ИСПРАВЛЕНО!
        builder.HasOne(a => a.Patient)
            .WithMany()
            .HasForeignKey("PatientId")
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey("DoctorId")
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes for performance
        builder.HasIndex(a => a.AppointmentDateTime);
            
        builder.HasIndex(a => a.RoomNumber);
            
        builder.HasIndex(a => a.RepeatAppointment);
            
        builder.HasIndex(a => new { a.Patient.Id, a.AppointmentDateTime });
            
        builder.HasIndex(a => new { a.Doctor.Id, a.AppointmentDateTime });
    }
}