using Polyclinic.Domain.Enums;

namespace Polyclinic.Domain.Subjects;

public class Doctor
{
    public int  Id { get; set; }
    public string PassportNumber { get; set; }
    public string FullName { get; set; }
    public int YearOfBirth { get; set; }
    public Specialization Specialization { get; set; }
    public int Experience { get; set; }
}