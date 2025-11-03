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
            .Where(doc => doc.Experience >= 10)
            .Select(doc => doc.Id)
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
            .Where(app => app.Doctor.Id == doctorId)
            .Select(app => app.Patient)
            .OrderBy(pat => pat.FullName)
            .Select(pat => pat.FullName)
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

        var result = fixture.Appointments.Count(app =>
            app.RepeatAppointment &&
            app.AppointmentDateTime >= monthStart &&
            app.AppointmentDateTime <= monthEnd);

        Assert.Equal(expectedCount, result);
    }
    
    // patient who age is more 30 and they appointed to some doctors 
    [Fact]
    public void PatientsAgeMore30WithMultipleDoctors()
    {
        var expectedNames = new List<string>
        {
            "Никитина Ольга Петровна"
        };

        var bornDate = new DateTime(1995, 1, 1); // 2025 - 30 

        var patients = fixture.Appointments
            .GroupBy(app => app.Patient)
            .Where(grpbpa => grpbpa.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(grpbpa => grpbpa.Key)
            .Where(pat => pat.DateOfBirth <= bornDate)
            .OrderBy(pat => pat.DateOfBirth)
            .Select(pat => pat.FullName)
            .ToList();

        Assert.Equal(expectedNames, patients);
    }
    
}