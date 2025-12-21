using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSQL.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd();
            
        builder.Property(d => d.PassportNumber)
            .HasMaxLength(20);
            
        builder.Property(d => d.FullName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(d => d.YearOfBirth);
            
        builder.Property(d => d.Experience);
            
        // Relationship with Specialization
        builder.HasOne(d => d.Specialization)
            .WithMany()
            .HasForeignKey("SpecializationId")
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder.HasIndex(d => d.PassportNumber)
            .IsUnique();
            
        builder.HasIndex(d => d.FullName);
            
        builder.HasIndex(d => d.Experience);
    }
}