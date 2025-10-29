using Polyclinic.Enums;

namespace Polyclinic.Subjects;

public class Doctor
{
    public string PasportNumber { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Specialization Specialization { get; set; }
    public int Experience { get; set; }
}