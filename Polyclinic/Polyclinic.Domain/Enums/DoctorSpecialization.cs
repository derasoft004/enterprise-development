namespace Polyclinic.Domain.Enums;

public class Specialization
{
    public int Id { get; set; }
    public required string Name { get; set; } // example Therapy, Surgery, Diagnostics, Emergency
}
