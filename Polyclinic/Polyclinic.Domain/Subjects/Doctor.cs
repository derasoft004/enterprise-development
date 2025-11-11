namespace Polyclinic.Domain.Subjects;

/// <summary>
///  Doctor model for the polyclinic 
/// </summary>
public class Doctor
{
    /// <summary>
    /// Unique Doctor identifier
    /// </summary>
    public required int  Id { get; set; }
    
    /// <summary>
    /// Doctor's passport number
    /// </summary>
    public required string PassportNumber { get; set; }
    
    /// <summary>
    /// Doctor's full name
    /// </summary>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Doctor's year of birth (optional)
    /// </summary>
    public int? YearOfBirth { get; set; }
    
    /// <summary>
    /// Doctor's specialization
    /// </summary>
    public required Specialization Specialization { get; set; }
    
    /// <summary>
    /// Doctor's work experience (optional)
    /// </summary>
    public int? Experience { get; set; }
}