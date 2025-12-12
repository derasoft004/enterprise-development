namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for updating Patient
/// </summary>
public class UpdatePatientRequest
{
    /// <summary>
    /// Patient's full name
    /// </summary>
    public required string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// Patient's residential address
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Patient's phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
}