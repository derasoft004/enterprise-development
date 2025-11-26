using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.InMemory.Seeders;

namespace Polyclinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Doctor Repository a repository for the Doctor class 
/// </summary>
public class InMemoryDoctorRepository : IRepository<Doctor, int>
{
    private readonly List<Doctor> _items = [];
    private int _currentId;

    public InMemoryDoctorRepository()
    {
        var specializations = InMemorySeeder.GetSpecializations();
        _items = InMemorySeeder.GetDoctors(specializations);
        _currentId = _items.Count > 0 ? _items.Max(d => d.Id) : 0;
    }

    public int Create(Doctor entity)
    {
        entity.Id = ++_currentId;
        _items.Add(entity);
        return entity.Id;
    }

    public List<Doctor> ReadAll()
    {
        return _items;
    }

    public Doctor? Read(int id)
    {
        return _items.FirstOrDefault(d => d.Id == id);
    }

    public Doctor? Update(int id, Doctor entity)
    {
        var existingEntity = Read(id);
        if (existingEntity == null) return null;

        existingEntity.PassportNumber = entity.PassportNumber;
        existingEntity.FullName = entity.FullName;
        existingEntity.YearOfBirth = entity.YearOfBirth;
        existingEntity.Specialization = entity.Specialization;
        existingEntity.Experience = entity.Experience;

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