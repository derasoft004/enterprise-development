using System.ComponentModel.DataAnnotations;
using Polyclinic.Domain.Enums;

namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for creating new Patient
/// </summary>
public class CreatePatientRequest
{
    /// <summary>
    /// Patient's passport number
    /// </summary>
    [Required(ErrorMessage = "Passport number is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Passport number must be between 5 and 20 characters")]
    public required string PassportNumber { get; set; }
    
    /// <summary>
    /// Patient's full name
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters")]
    public required string FullName { get; set; }
    
    /// <summary>
    /// Patient's date of birth
    /// </summary>
    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public required DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// Patient's gender
    /// </summary>
    [EnumDataType(typeof(Gender))]
    public Gender? Gender { get; set; }
    
    /// <summary>
    /// Patient's residential address
    /// </summary>
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string? Address { get; set; }
    
    /// <summary>
    /// Patient's blood group
    /// </summary>
    [EnumDataType(typeof(BloodGroup))]
    public BloodGroup? BloodGroup { get; set; }
    
    /// <summary>
    /// Patient's blood Rh factor
    /// </summary>
    [EnumDataType(typeof(ResusFactor))]
    public ResusFactor? ResusFactor { get; set; }
    
    /// <summary>
    /// Patient's phone number
    /// </summary>
    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
    public string? PhoneNumber { get; set; }
}