using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for creating new Doctor
/// </summary>
public class CreateDoctorRequest
{
    /// <summary>
    /// Doctor's passport number
    /// </summary>
    [Required(ErrorMessage = "Passport number is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Passport number must be between 5 and 20 characters")]
    public required string PassportNumber { get; set; }
    
    /// <summary>
    /// Doctor's full name
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters")]
    public required string FullName { get; set; }
    
    /// <summary>
    /// Doctor's year of birth
    /// </summary>
    [Range(1900, 2100, ErrorMessage = "Year of birth must be between 1900 and 2100")]
    public int? YearOfBirth { get; set; }
    
    /// <summary>
    /// Doctor's specialization identifier
    /// </summary>
    [Required(ErrorMessage = "Specialization ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Specialization ID must be a positive number")]
    public required int SpecializationId { get; set; }
    
    /// <summary>
    /// Doctor's work experience in years
    /// </summary>
    [Range(0, 100, ErrorMessage = "Experience must be between 0 and 100 years")]
    public int? Experience { get; set; }
}