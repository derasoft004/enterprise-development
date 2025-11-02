using Xunit;
using Polyclinic.Subjects;

namespace Polyclinic.Tests;

// class with unit-tests on data from dataseed 
public class PolyclinicTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    // doctors with experience more 10 years
    [Fact]
    public void DoctorsExperienceMore10()
    {
        List<int> expectedId = [1, 2, 3, 5, 6];

        var experiencedId = fixture.Doctors
            .Where(d => d.Experience >= 10)
            .Select(d => d.Id)
            .Order()
            .ToList();

        Assert.NotNull(experiencedId);
        Assert.Equal(expectedId, experiencedId);
    }
    
    // patients by appointments to some doctors by doctor's id
    [Fact]
    public void PatientsByDoctor()
    {
        const int doctorId = 6;
        var expectedNames = new List<string> 
        {
            "Михайлова Анна Романовна",
            "Макарова Виктория Андреевна"
        };

        var result = fixture.Appointments
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .OrderBy(p => p.FullName)
            .Select(p => p.FullName)
            .ToList();

        Assert.Equal(expectedNames, result);
    }
    
}