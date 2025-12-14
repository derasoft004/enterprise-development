namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for Appointment entity (response)
/// </summary>
public class AppointmentDto
{
    /// <summary>
    /// Appointment unique identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Patient's full name
    /// </summary>
    public required string  PatientName { get; set; }
    
    /// <summary>
    /// Doctor's full name
    /// </summary>
    public string? DoctorName { get; set; }
    
    /// <summary>
    /// Date and time of the appointment
    /// </summary>
    public DateTime AppointmentDateTime { get; set; }
    
    /// <summary>
    /// Room number where appointment takes place
    /// </summary>
    public int? RoomNumber { get; set; }
    
    /// <summary>
    /// Indicates if this is a repeat appointment
    /// </summary>
    public bool IsRepeat { get; set; }
}