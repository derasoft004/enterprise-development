using Polyclinic.Domain.Enums;

namespace Polyclinic.Domain.Subjects;

public class Patient
{
    public int Id { get; set; }
    public string PassportNumber { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public BloodGroup BloodGroup { get; set; }
    public ResusFactor ResusFactor { get; set; }
    public string PhoneNumber { get; set; }
}