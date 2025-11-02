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
    
    // repeated appointments at last month 
    [Fact]
    public void RepeatAppointmentsCountLastMonth()
    {
        const int expectedCount = 2; // 2 true and 1 false
        var monthStart = new DateTime(2025, 12, 1);
        var monthEnd = new DateTime(2025, 12, 31);

        var result = fixture.Appointments.Count(a =>
            a.RepeatAppointment &&
            a.AppointmentDateTime >= monthStart &&
            a.AppointmentDateTime <= monthEnd);

        Assert.Equal(expectedCount, result);
    }
    
    
}