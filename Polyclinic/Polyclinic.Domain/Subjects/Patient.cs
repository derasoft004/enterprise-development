using Polyclinic.Domain.Enums;

namespace Polyclinic.Domain.Subjects;

/// <summary>
///  Patient model for the polyclinic 
/// </summary>
public class Patient
{
    /// <summary>
    /// Unique Patient identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Patient's passport number
    /// </summary>
    public string? PassportNumber { get; set; }
    
    /// <summary>
    /// Patient's full name
    /// </summary>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Patient's gender (optional)
    /// </summary>
    public Gender? Gender { get; set; }
    
    /// <summary>
    /// Patient's date of birth
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// Patient's residential address (optional)
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Patient's blood group (optional)
    /// </summary>
    public BloodGroup? BloodGroup { get; set; }
    
    /// <summary>
    /// Patient's blood Rh factor (optional)
    /// </summary>
    public ResusFactor? ResusFactor { get; set; }
    
    /// <summary>
    /// Patient's phone number (optional)
    /// </summary>
    public string? PhoneNumber { get; set; }
}