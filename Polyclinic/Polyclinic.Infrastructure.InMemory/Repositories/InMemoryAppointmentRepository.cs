using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.InMemory.Seeders;

namespace Polyclinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Appointment Repository a repository for the Appointment class 
/// </summary>
public class InMemoryAppointmentRepository : IRepository<Appointment, int>
{
    private readonly List<Appointment> _items = [];
    private int _currentId;

    public InMemoryAppointmentRepository()
    {
        var patients = InMemorySeeder.GetPatients();
        var specializations = InMemorySeeder.GetSpecializations();
        var doctors = InMemorySeeder.GetDoctors(specializations);
        _items = InMemorySeeder.GetAppointments(patients, doctors);
        _currentId = _items.Count > 0 ? _items.Max(a => a.Id) : 0;
    }

    public int Create(Appointment entity)
    {
        entity.Id = ++_currentId;
        _items.Add(entity);
        return entity.Id;
    }

    public List<Appointment> ReadAll()
    {
        return _items;
    }

    public Appointment? Read(int id)
    {
        return _items.FirstOrDefault(a => a.Id == id);
    }

    public Appointment? Update(int id, Appointment entity)
    {
        var existingEntity = Read(id);
        if (existingEntity == null) return null;

        existingEntity.Patient = entity.Patient;
        existingEntity.Doctor = entity.Doctor;
        existingEntity.AppointmentDateTime = entity.AppointmentDateTime;
        existingEntity.RoomNumber = entity.RoomNumber;
        existingEntity.RepeatAppointment = entity.RepeatAppointment;

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