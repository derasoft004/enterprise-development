namespace Polyclinic.Domain.Subjects;

/// <summary>
/// Doctor's specialization
/// </summary>
public class Specialization
{
    public int Id { get; set; }
    
    /// <summary>
    /// Doctor's specialization's name (example Therapy, Surgery, Diagnostics, Emergency)
    /// </summary>
    public required string Name { get; set; } 
}
