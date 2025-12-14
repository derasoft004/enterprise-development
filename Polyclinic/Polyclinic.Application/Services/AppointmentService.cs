using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service implementation for Appointment business operations
/// </summary>
public class AppointmentService(
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Patient, int> patientRepository, 
    IRepository<Doctor, int> doctorRepository
    ) : IAppointmentService
{
    public List<AppointmentDto> GetAllAppointments()
    {
        var appointments = appointmentRepository.ReadAll();
        return [..appointments.Select(MapToDto)];
    }

    public AppointmentDto? GetAppointmentById(int id)
    {
        var appointment = appointmentRepository.Read(id);
        return appointment == null ? null : MapToDto(appointment);
    }

    public AppointmentDto CreateAppointment(CreateAppointmentRequest createRequest)
    {
        var patient = patientRepository.Read(createRequest.PatientId)
                      ?? throw new ArgumentException($"Patient with ID {createRequest.PatientId} not found");
        
        var doctor = doctorRepository.Read(createRequest.DoctorId)
                      ?? throw new ArgumentException($"Doctor with ID {createRequest.DoctorId} not found");
        
        var appointment = new Appointment
        {
            Patient = patient,
            Doctor = doctor,
            AppointmentDateTime = createRequest.AppointmentDateTime,
            RoomNumber = createRequest.RoomNumber,
            RepeatAppointment = createRequest.IsRepeat
        };

        var id = appointmentRepository.Create(appointment);
        return MapToDto(appointment);
    }

    public AppointmentDto? UpdateAppointment(int id, UpdateAppointmentRequest updateRequest)
    {
        var existing = appointmentRepository.Read(id);
        if (existing == null) return null;
        
        var patient = patientRepository.Read(updateRequest.PatientId)
                      ?? throw new ArgumentException($"Patient with ID {updateRequest.PatientId} not found");
        
        var doctor = doctorRepository.Read(updateRequest.DoctorId)
                     ?? throw new ArgumentException($"Doctor with ID {updateRequest.DoctorId} not found");

        existing.Patient = patient;
        existing.Doctor = doctor;
        existing.AppointmentDateTime = updateRequest.AppointmentDateTime;
        existing.RoomNumber = updateRequest.RoomNumber;
        existing.RepeatAppointment = updateRequest.IsRepeat;

        var updated = appointmentRepository.Update(id, existing);
        return updated == null ? null : MapToDto(updated);
    }

    public bool DeleteAppointment(int id)
    {
        return appointmentRepository.Delete(id);
    }

    public List<AppointmentDto> GetAppointmentsByDoctor(int doctorId)
    {
        var appointments = appointmentRepository.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(MapToDto)
            .ToList();

        return appointments;
    }

    public int GetRepeatAppointmentsCountForMonth(int year, int month)
    {
        var count = appointmentRepository.ReadAll()
            .Count(a => a.RepeatAppointment && 
                       a.AppointmentDateTime.Year == year && 
                       a.AppointmentDateTime.Month == month);

        return count;
    }

    public List<AppointmentDto> GetAppointmentsInRoomForMonth(int roomNumber, int year, int month)
    {
        var appointments = appointmentRepository.ReadAll()
            .Where(a => a.RoomNumber == roomNumber &&
                       a.AppointmentDateTime.Year == year && 
                       a.AppointmentDateTime.Month == month)
            .Select(MapToDto)
            .ToList();

        return appointments;
    }

    private static AppointmentDto MapToDto(Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            PatientName = appointment.Patient.FullName,
            DoctorName = appointment.Doctor.FullName,
            AppointmentDateTime = appointment.AppointmentDateTime,
            RoomNumber = appointment.RoomNumber,
            IsRepeat = appointment.RepeatAppointment
        };
    }
}