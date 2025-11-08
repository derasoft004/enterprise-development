namespace Polyclinic.Domain.Enums;

/// <summary>
/// Doctor's specialization
/// </summary>
public class Specialization
{
    public int Id { get; set; }
    public required string Name { get; set; } // example Therapy, Surgery, Diagnostics, Emergency
}
