using Polyclinic.Domain.Enums;

namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for Patient entity (response)
/// </summary>
public class PatientDto
{
    /// <summary>
    /// Patient unique identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Patient's full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// Patient's age (calculated from DateOfBirth)
    /// </summary>
    public int Age { get; set; }
    
    /// <summary>
    /// Patient's phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// Patient's residential address
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Patient's gender
    /// </summary>
    public Gender? Gender { get; set; }
}