namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Data Transfer Object for Doctor entity (response)
/// </summary>
public class DoctorDto
{
    /// <summary>
    /// Doctor unique identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Doctor's full name
    /// </summary>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Doctor's specialization name
    /// </summary>
    public string? Specialization { get; set; }
    
    /// <summary>
    /// Doctor's work experience in years
    /// </summary>
    public int? Experience { get; set; }
    
    /// <summary>
    /// Doctor's experience level (Junior/Middle/Senior)
    /// </summary>
    public string? ExperienceLevel { get; set; }
}