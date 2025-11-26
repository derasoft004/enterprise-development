using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.InMemory.Seeders;

namespace Polyclinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Specialization Repository for the Specialization class 
/// </summary>
public class InMemorySpecializationRepository : IRepository<Specialization, int>
{
    private readonly List<Specialization> _items = [];
    private int _currentId;

    public InMemorySpecializationRepository()
    {
        _items = InMemorySeeder.GetSpecializations();
        _currentId = _items.Count > 0 ? _items.Max(s => s.Id) : 0;
    }

    public int Create(Specialization entity)
    {
        entity.Id = ++_currentId;
        _items.Add(entity);
        return entity.Id;
    }

    public List<Specialization> ReadAll()
    {
        return _items;
    }

    public Specialization? Read(int id)
    {
        return _items.FirstOrDefault(s => s.Id == id);
    }

    public Specialization? Update(int id, Specialization entity)
    {
        var existingEntity = Read(id);
        if (existingEntity == null) return null;

        existingEntity.Name = entity.Name;

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