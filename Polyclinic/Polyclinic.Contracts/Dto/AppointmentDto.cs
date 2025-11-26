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
    public string PatientName { get; set; } = string.Empty;
    
    /// <summary>
    /// Doctor's full name
    /// </summary>
    public string DoctorName { get; set; } = string.Empty;
    
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

/// <summary>
/// Data Transfer Object for creating new Appointment
/// </summary>
public class CreateAppointmentRequest
{
    /// <summary>
    /// Patient identifier
    /// </summary>
    public required int PatientId { get; set; }
    
    /// <summary>
    /// Doctor identifier
    /// </summary>
    public required int DoctorId { get; set; }
    
    /// <summary>
    /// Date and time of the appointment
    /// </summary>
    public required DateTime AppointmentDateTime { get; set; }
    
    /// <summary>
    /// Room number where appointment takes place
    /// </summary>
    public int? RoomNumber { get; set; }
    
    /// <summary>
    /// Indicates if this is a repeat appointment
    /// </summary>
    public bool IsRepeat { get; set; }
}

/// <summary>
/// Data Transfer Object for updating Appointment
/// </summary>
public class UpdateAppointmentRequest
{
    /// <summary>
    /// Patient identifier
    /// </summary>
    public required int PatientId { get; set; }
    
    /// <summary>
    /// Doctor identifier
    /// </summary>
    public required int DoctorId { get; set; }
    
    /// <summary>
    /// Date and time of the appointment
    /// </summary>
    public required DateTime AppointmentDateTime { get; set; }
    
    /// <summary>
    /// Room number where appointment takes place
    /// </summary>
    public int? RoomNumber { get; set; }
    
    /// <summary>
    /// Indicates if this is a repeat appointment
    /// </summary>
    public bool IsRepeat { get; set; }
}