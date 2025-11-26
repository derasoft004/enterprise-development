using Polyclinic.Contracts.Dto;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Service interface for Appointment business operations
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Get all appointments
    /// </summary>
    public List<AppointmentDto> GetAllAppointments();

    /// <summary>
    /// Get appointment by identifier
    /// </summary>
    public AppointmentDto? GetAppointmentById(int id);

    /// <summary>
    /// Create new appointment
    /// </summary>
    public AppointmentDto CreateAppointment(CreateAppointmentRequest createRequest);

    /// <summary>
    /// Update existing appointment
    /// </summary>
    public AppointmentDto? UpdateAppointment(int id, UpdateAppointmentRequest updateRequest);

    /// <summary>
    /// Delete appointment by identifier
    /// </summary>
    public bool DeleteAppointment(int id);

    /// <summary>
    /// Get appointments by doctor identifier
    /// </summary>
    public List<AppointmentDto> GetAppointmentsByDoctor(int doctorId);

    /// <summary>
    /// Get repeat appointments count for specified month
    /// </summary>
    public int GetRepeatAppointmentsCountForMonth(int year, int month);

    /// <summary>
    /// Get appointments in room for specified month
    /// </summary>
    public List<AppointmentDto> GetAppointmentsInRoomForMonth(int roomNumber, int year, int month);
}