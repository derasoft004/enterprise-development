using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSql.Repository;

public class PostgresPatientRepository(PolyclinicDbContext context) : IRepository<Patient, int>
{
    /// <summary>
    /// Create new patient
    /// </summary>
    public int Create(Patient entity)
    {
        context.Patients.Add(entity);
        context.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Read all patients
    /// </summary>
    public List<Patient> ReadAll()
    {
        return [.. context.Patients];
    }

    /// <summary>
    /// Read patient by id
    /// </summary>
    public Patient? Read(int id)
    {
        return context.Patients.Find(id);
    }

    /// <summary>
    /// Update patient by id
    /// </summary>
    public Patient? Update(int id, Patient entity)
    {
        var existingPatient = context.Patients.Find(id);
        if (existingPatient == null)
            return null;

        // Update properties
        existingPatient.PassportNumber = entity.PassportNumber;
        existingPatient.FullName = entity.FullName;
        existingPatient.Gender = entity.Gender;
        existingPatient.DateOfBirth = entity.DateOfBirth;
        existingPatient.Address = entity.Address;
        existingPatient.PhoneNumber = entity.PhoneNumber;
        existingPatient.BloodGroup = entity.BloodGroup;
        existingPatient.ResusFactor = entity.ResusFactor;

        context.SaveChanges();
        return existingPatient;
    }

    /// <summary>
    /// Delete patient by id
    /// </summary>
    public bool Delete(int id)
    {
        var patient = context.Patients.Find(id);
        if (patient == null)
            return false;

        context.Patients.Remove(patient);
        context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Get patient by passport number (дополнительный метод)
    /// </summary>
    public Patient? GetByPassportNumber(string passportNumber)
    {
        return context.Patients
            .FirstOrDefault(p => p.PassportNumber == passportNumber);
    }
}