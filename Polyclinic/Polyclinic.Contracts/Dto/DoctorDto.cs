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
    public string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// Doctor's specialization name
    /// </summary>
    public string Specialization { get; set; } = string.Empty;
    
    /// <summary>
    /// Doctor's work experience in years
    /// </summary>
    public int Experience { get; set; }
    
    /// <summary>
    /// Doctor's experience level (Junior/Middle/Senior)
    /// </summary>
    public string ExperienceLevel { get; set; } = string.Empty;
}

/// <summary>
/// Data Transfer Object for creating new Doctor
/// </summary>
public class CreateDoctorRequest
{
    /// <summary>
    /// Doctor's passport number
    /// </summary>
    public required string PassportNumber { get; set; } = string.Empty;
    
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