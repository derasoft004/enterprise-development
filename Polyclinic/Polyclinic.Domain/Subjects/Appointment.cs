namespace Polyclinic.Domain.Subjects;

/// <summary>
///  Appointment model for the polyclinic 
/// </summary>
public class Appointment
{
    /// <summary>
    /// Unique Appointment identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Patient has an Appointment
    /// </summary>
    public required Patient Patient { get; set; }
    
    /// <summary>
    /// DateTime of the Appointment
    /// </summary>
    public DateTime AppointmentDateTime { get; set; }
    
    /// <summary>
    /// Doctor of the Appointment
    /// </summary>
    public required Doctor Doctor { get; set; }
    
    /// <summary>
    /// RoomNumber of the Appointment
    /// </summary>
    public int? RoomNumber { get; set; }
    
    /// <summary>
    /// Repeated whether this Appointment
    /// </summary>
    public bool RepeatAppointment { get; set; }
}