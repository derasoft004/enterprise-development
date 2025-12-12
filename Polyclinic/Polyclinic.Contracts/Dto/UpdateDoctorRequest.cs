namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for updating Doctor
/// </summary>
public class UpdateDoctorRequest
{
    /// <summary>
    /// Doctor's full name
    /// </summary>
    public required string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// Doctor's year of birth
    /// </summary>
    public int? YearOfBirth { get; set; }
    
    /// <summary>
    /// Doctor's specialization identifier
    /// </summary>
    public required int SpecializationId { get; set; }
    
    /// <summary>
    /// Doctor's work experience in years
    /// </summary>
    public int? Experience { get; set; }
}