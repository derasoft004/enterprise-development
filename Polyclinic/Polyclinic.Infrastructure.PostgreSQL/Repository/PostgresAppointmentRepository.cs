using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSQL.Repository;

public class PostgresAppointmentRepository(PolyclinicDbContext context) : IRepository<Appointment, int>
{
    /// <summary>
    /// Create new appointment
    /// </summary>
    public int Create(Appointment entity)
    {
        context.Appointments.Add(entity);
        context.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Read all appointments
    /// </summary>
    public List<Appointment> ReadAll()
    {
        return context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Specialization)
            .ToList();
    }

    /// <summary>
    /// Read appointment by id
    /// </summary>
    public Appointment? Read(int id)
    {
        return context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Specialization)
            .FirstOrDefault(a => a.Id == id);
    }

    /// <summary>
    /// Update appointment by id
    /// </summary>
    public Appointment? Update(int id, Appointment entity)
    {
        var existingAppointment = context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefault(a => a.Id == id);
        
        if (existingAppointment == null)
            return null;

        // Update properties
        existingAppointment.Patient = entity.Patient;
        existingAppointment.Doctor = entity.Doctor;
        existingAppointment.AppointmentDateTime = entity.AppointmentDateTime;
        existingAppointment.RoomNumber = entity.RoomNumber;
        existingAppointment.RepeatAppointment = entity.RepeatAppointment;

        context.SaveChanges();
        return existingAppointment;
    }

    /// <summary>
    /// Delete appointment by id
    /// </summary>
    public bool Delete(int id)
    {
        var appointment = context.Appointments.Find(id);
        if (appointment == null)
            return false;

        context.Appointments.Remove(appointment);
        context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Get appointments by date range
    /// </summary>
    public List<Appointment> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        return context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Specialization)
            .Where(a => a.AppointmentDateTime >= startDate && 
                       a.AppointmentDateTime <= endDate)
            .ToList();
    }

    /// <summary>
    /// Get appointments by doctor ID
    /// </summary>
    public List<Appointment> GetByDoctorId(int doctorId)
    {
        return context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Specialization)
            .Where(a => a.Doctor.Id == doctorId)
            .ToList();
    }

    /// <summary>
    /// Get appointments by patient ID
    /// </summary>
    public List<Appointment> GetByPatientId(int patientId)
    {
        return context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Specialization)
            .Where(a => a.Patient.Id == patientId)
            .ToList();
    }

    /// <summary>
    /// Get repeat appointments count for specific month
    /// </summary>
    public int GetRepeatAppointmentsCount(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        
        return context.Appointments
            .Count(a => a.AppointmentDateTime >= startDate && 
                       a.AppointmentDateTime <= endDate &&
                       a.RepeatAppointment);
    }
}