using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSQL.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable("Specializations");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();
            
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        // Index
        builder.HasIndex(s => s.Name)
            .IsUnique();
    }
}