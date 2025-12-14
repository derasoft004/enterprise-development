using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for updating Patient
/// </summary>
public class UpdatePatientRequest
{
    /// <summary>
    /// Patient's full name
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters")]
    public required string FullName { get; set; }
    
    /// <summary>
    /// Patient's residential address
    /// </summary>
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string? Address { get; set; }
    
    /// <summary>
    /// Patient's phone number
    /// </summary>
    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
    public string? PhoneNumber { get; set; }
}