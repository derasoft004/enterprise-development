using Polyclinic.Domain.Enums;

namespace Polyclinic.Domain.Subjects;

public class Doctor
{
    public required int  Id { get; set; }
    public required string PassportNumber { get; set; }
    public required string FullName { get; set; }
    public int? YearOfBirth { get; set; }
    public required Specialization Specialization { get; set; }
    public int? Experience { get; set; }
}