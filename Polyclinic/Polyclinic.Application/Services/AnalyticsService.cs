using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service implementation for analytical operations
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly IRepository<Doctor, int> _doctorRepository;
    private readonly IRepository<Appointment, int> _appointmentRepository;
    private readonly IRepository<Patient, int> _patientRepository;

    public AnalyticsService(
        IRepository<Doctor, int> doctorRepository,
        IRepository<Appointment, int> appointmentRepository,
        IRepository<Patient, int> patientRepository)
    {
        _doctorRepository = doctorRepository;
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
    }

    public List<DoctorDto> GetDoctorsWithExperienceMoreThan(int years)
    {
        var doctors = _doctorRepository.ReadAll()
            .Where(d => d.Experience >= years)
            .Select(MapDoctorToDto)
            .ToList();

        return doctors;
    }

    public List<PatientDto> GetPatientsByDoctor(int doctorId)
    {
        var patients = _appointmentRepository.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .Distinct()
            .Select(MapPatientToDto)
            .ToList();

        return patients;
    }

    public int GetRepeatAppointmentsCountForMonth(int year, int month)
    {
        var count = _appointmentRepository.ReadAll()
            .Count(a => a.RepeatAppointment && 
                       a.AppointmentDateTime.Year == year && 
                       a.AppointmentDateTime.Month == month);

        return count;
    }

    public List<PatientDto> GetPatientsOverAgeWithMultipleDoctors(int age)
    {
        var cutoffDate = DateTime.Now.AddYears(-age);
        var appointments = _appointmentRepository.ReadAll();

        var patients = appointments
            .GroupBy(a => a.Patient)
            .Where(g => g.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(g => g.Key)
            .Where(p => p.DateOfBirth <= cutoffDate)
            .Select(MapPatientToDto)
            .ToList();

        return patients;
    }

    public List<AppointmentDto> GetAppointmentsInRoomForMonth(int roomNumber, int year, int month)
    {
        var appointments = _appointmentRepository.ReadAll()
            .Where(a => a.RoomNumber == roomNumber &&
                       a.AppointmentDateTime.Year == year && 
                       a.AppointmentDateTime.Month == month)
            .Select(MapAppointmentToDto)
            .ToList();

        return appointments;
    }

    private static DoctorDto MapDoctorToDto(Doctor doctor)
    {
        var experienceLevel = doctor.Experience switch
        {
            < 5 => "Junior",
            < 15 => "Middle",
            _ => "Senior"
        };

        return new DoctorDto
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Specialization = doctor.Specialization.Name,
            Experience = doctor.Experience ?? 0,
            ExperienceLevel = experienceLevel
        };
    }

    private static PatientDto MapPatientToDto(Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            FullName = patient.FullName,
            Age = DateTime.Now.Year - patient.DateOfBirth.Year,
            PhoneNumber = patient.PhoneNumber,
            Address = patient.Address,
            Gender = patient.Gender
        };
    }

    private static AppointmentDto MapAppointmentToDto(Appointment appointment)
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