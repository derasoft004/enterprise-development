using Xunit;
using Polyclinic.Subjects;

namespace Polyclinic.Tests;

public class PolyclinicTests
{
    public List<Doctor> Doctors = DataSeed.Doctors;
    public List<Patient> Patients =  DataSeed.Patients;
    public List<Appointment> Appointments = DataSeed.Appointments;
    
    [Fact]
    public void DoctorsExperienceMore10()
    {
        List<int> expectedId = [1, 2, 3, 5, 6];

        var experiencedId = Doctors
            .Where(d => d.Experience >= 10)
            .Select(d => d.Id)
            .Order()
            .ToList();

        Assert.NotNull(experiencedId);
        Assert.Equal(expectedId, experiencedId);
    }
}
