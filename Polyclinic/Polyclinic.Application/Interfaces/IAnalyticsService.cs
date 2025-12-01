using Polyclinic.Contracts.Dto;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Service interface for analytical operations
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Get doctors with experience more than specified years
    /// </summary>
    public List<DoctorDto> GetDoctorsWithExperienceMoreThan(int years);

    /// <summary>
    /// Get patients by doctor identifier
    /// </summary>
    public List<PatientDto> GetPatientsByDoctor(int doctorId);

    /// <summary>
    /// Get repeat appointments count for specified month
    /// </summary>
    public int GetRepeatAppointmentsCountForMonth(int year, int month);

    /// <summary>
    /// Get patients over specified age with multiple doctors
    /// </summary>
    public List<PatientDto> GetPatientsOverAgeWithMultipleDoctors(int age);

    /// <summary>
    /// Get appointments in room for specified month
    /// </summary>
    public List<AppointmentDto> GetAppointmentsInRoomForMonth(int roomNumber, int year, int month);
}