using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service implementation for Appointment business operations
/// </summary>
public class AppointmentService : IAppointmentService
{
    private readonly IRepository<Appointment, int> _appointmentRepository;
    private readonly IRepository<Patient, int> _patientRepository;
    private readonly IRepository<Doctor, int> _doctorRepository;

    public AppointmentService(
        IRepository<Appointment, int> appointmentRepository,
        IRepository<Patient, int> patientRepository,
        IRepository<Doctor, int> doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }

    public List<AppointmentDto> GetAllAppointments()
    {
        var appointments = _appointmentRepository.ReadAll();
        return appointments.Select(MapToDto).ToList();
    }

    public AppointmentDto? GetAppointmentById(int id)
    {
        var appointment = _appointmentRepository.Read(id);
        return appointment == null ? null : MapToDto(appointment);
    }

    public AppointmentDto CreateAppointment(CreateAppointmentRequest createRequest)
    {
        var patient = _patientRepository.Read(createRequest.PatientId);
        if (patient == null)
            throw new ArgumentException($"Patient with ID {createRequest.PatientId} not found");

        var doctor = _doctorRepository.Read(createRequest.DoctorId);
        if (doctor == null)
            throw new ArgumentException($"Doctor with ID {createRequest.DoctorId} not found");

        var appointment = new Appointment
        {
            Patient = patient,
            Doctor = doctor,
            AppointmentDateTime = createRequest.AppointmentDateTime,
            RoomNumber = createRequest.RoomNumber,
            RepeatAppointment = createRequest.IsRepeat
        };

        var id = _appointmentRepository.Create(appointment);
        return MapToDto(appointment);
    }

    public AppointmentDto? UpdateAppointment(int id, UpdateAppointmentRequest updateRequest)
    {
        var existing = _appointmentRepository.Read(id);
        if (existing == null) return null;

        var patient = _patientRepository.Read(updateRequest.PatientId);
        if (patient == null)
            throw new ArgumentException($"Patient with ID {updateRequest.PatientId} not found");

        var doctor = _doctorRepository.Read(updateRequest.DoctorId);
        if (doctor == null)
            throw new ArgumentException($"Doctor with ID {updateRequest.DoctorId} not found");

        existing.Patient = patient;
        existing.Doctor = doctor;
        existing.AppointmentDateTime = updateRequest.AppointmentDateTime;
        existing.RoomNumber = updateRequest.RoomNumber;
        existing.RepeatAppointment = updateRequest.IsRepeat;

        var updated = _appointmentRepository.Update(id, existing);
        return updated == null ? null : MapToDto(updated);
    }

    public bool DeleteAppointment(int id)
    {
        return _appointmentRepository.Delete(id);
    }

    public List<AppointmentDto> GetAppointmentsByDoctor(int doctorId)
    {
        var appointments = _appointmentRepository.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(MapToDto)
            .ToList();

        return appointments;
    }

    public int GetRepeatAppointmentsCountForMonth(int year, int month)
    {
        var count = _appointmentRepository.ReadAll()
            .Count(a => a.RepeatAppointment && 
                       a.AppointmentDateTime.Year == year && 
                       a.AppointmentDateTime.Month == month);

        return count;
    }

    public List<AppointmentDto> GetAppointmentsInRoomForMonth(int roomNumber, int year, int month)
    {
        var appointments = _appointmentRepository.ReadAll()
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