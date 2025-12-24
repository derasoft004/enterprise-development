using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSql.Repository;

public class PostgresDoctorRepository(PolyclinicDbContext context) : IRepository<Doctor, int>
{
    /// <summary>
    /// Create new doctor
    /// </summary>
    public int Create(Doctor entity)
    {
        context.Doctors.Add(entity);
        context.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Read all doctors
    /// </summary>
    public List<Doctor> ReadAll()
    {
        return [.. context.Doctors.Include(d => d.Specialization)];
    }

    /// <summary>
    /// Read doctor by id
    /// </summary>
    public Doctor? Read(int id)
    {
        return context.Doctors
            .Include(d => d.Specialization)
            .FirstOrDefault(d => d.Id == id);
    }

    /// <summary>
    /// Update doctor by id
    /// </summary>
    public Doctor? Update(int id, Doctor entity)
    {
        var existingDoctor = context.Doctors
            .Include(d => d.Specialization)
            .FirstOrDefault(d => d.Id == id);
        
        if (existingDoctor == null)
            return null;

        // Update properties
        existingDoctor.PassportNumber = entity.PassportNumber;
        existingDoctor.FullName = entity.FullName;
        existingDoctor.YearOfBirth = entity.YearOfBirth;
        existingDoctor.Specialization = entity.Specialization;
        existingDoctor.Experience = entity.Experience;

        context.SaveChanges();
        return existingDoctor;
    }

    /// <summary>
    /// Delete doctor by id
    /// </summary>
    public bool Delete(int id)
    {
        var doctor = context.Doctors.Find(id);
        if (doctor == null)
            return false;

        context.Doctors.Remove(doctor);
        context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Get doctors with experience more than specified years
    /// </summary>
    public List<Doctor> GetByExperience(int years)
    {
        return [.. context.Doctors
            .Include(d => d.Specialization)
            .Where(d => d.Experience.HasValue && d.Experience.Value > years)];
    }
}