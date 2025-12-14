using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for creating new Appointment
/// </summary>
public class CreateAppointmentRequest
{
    /// <summary>
    /// Patient identifier
    /// </summary>
    [Required(ErrorMessage = "Patient ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be a positive number")]
    public required int PatientId { get; set; }
    
    /// <summary>
    /// Doctor identifier
    /// </summary>
    [Required(ErrorMessage = "Doctor ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be a positive number")]
    public required int DoctorId { get; set; }
    
    /// <summary>
    /// Date and time of the appointment
    /// </summary>
    [Required(ErrorMessage = "Appointment date and time is required")]
    [DataType(DataType.DateTime)]
    public required DateTime AppointmentDateTime { get; set; }
    
    /// <summary>
    /// Room number where appointment takes place
    /// </summary>
    [Range(1, 1000, ErrorMessage = "Room number must be between 1 and 1000")]
    public int? RoomNumber { get; set; }
    
    /// <summary>
    /// Indicates if this is a repeat appointment
    /// </summary>
    public bool IsRepeat { get; set; }
}