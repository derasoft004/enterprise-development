using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.InMemory.Seeders;

namespace Polyclinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Patient Repository for the Patient class 
/// </summary>
public class InMemoryPatientRepository : IRepository<Patient, int>
{
    private readonly List<Patient> _items = [];
    private int _currentId;

    /// <summary>
    /// A constructor that uses data from InMemorySeeder
    /// </summary>
    public InMemoryPatientRepository()
    {
        _items = InMemorySeeder.GetPatients();
        _currentId = _items.Count > 0 ? _items.Max(p => p.Id) : 0;
    }

    public int Create(Patient entity)
    {
        entity.Id = ++_currentId;
        _items.Add(entity);
        return entity.Id;
    }

    public List<Patient> ReadAll()
    {
        return _items;
    }

    public Patient? Read(int id)
    {
        return _items.FirstOrDefault(p => p.Id == id);
    }

    public Patient? Update(int id, Patient entity)
    {
        var existingEntity = Read(id);
        if (existingEntity == null) return null;

        // Обновляем все поля кроме Id
        existingEntity.PassportNumber = entity.PassportNumber;
        existingEntity.FullName = entity.FullName;
        existingEntity.Gender = entity.Gender;
        existingEntity.DateOfBirth = entity.DateOfBirth;
        existingEntity.Address = entity.Address;
        existingEntity.BloodGroup = entity.BloodGroup;
        existingEntity.ResusFactor = entity.ResusFactor;
        existingEntity.PhoneNumber = entity.PhoneNumber;

        return existingEntity;
    }

    public bool Delete(int id)
    {
        var existingEntity = Read(id);
        if (existingEntity == null) return false;

        _items.Remove(existingEntity);
        return true;
    }
}