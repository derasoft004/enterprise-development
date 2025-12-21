using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSQL.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
            
        builder.Property(p => p.PassportNumber)
            .HasMaxLength(20);
            
        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.Gender)
            .HasConversion<string>()
            .HasMaxLength(10);
            
        builder.Property(p => p.DateOfBirth)
            .IsRequired();
            
        builder.Property(p => p.Address)
            .HasMaxLength(200);
            
        builder.Property(p => p.BloodGroup)
            .HasConversion<string>()
            .HasMaxLength(5);
            
        builder.Property(p => p.ResusFactor)
            .HasConversion<string>()
            .HasMaxLength(10);
            
        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20);
            
        // Indexes
        builder.HasIndex(p => p.PassportNumber)
            .IsUnique();
            
        builder.HasIndex(p => p.FullName);
            
        builder.HasIndex(p => p.DateOfBirth);
    }
}