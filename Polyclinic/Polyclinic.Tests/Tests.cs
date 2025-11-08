using Xunit;

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

        var result = fixture.Appointments
            .Where(app => app.Doctor.Id == doctorId)
            .Select(app => app.Patient)
            .OrderBy(pat => pat.FullName)
            .Select(pat => pat.FullName)
            .ToList();

        var expectedNames = new List<string> 
        {
            "Михайлова Анна Романовна",
            "Макарова Виктория Андреевна"
        };
        Assert.Equal(expectedNames, result);
    }
    
    // repeated appointments at last month 
    [Fact]
    public void RepeatAppointmentsCountLastMonth()
    {
        var month = new DateTime(2025, 12, 1);

        var result = fixture.Appointments.Count(app =>
            app.RepeatAppointment &&
            app.AppointmentDateTime.Year == month.Year &&
            app.AppointmentDateTime.Month == month.Month);
        
        const int expectedCount = 2; // 2 true and 1 false
        Assert.Equal(expectedCount, result);
    }
    
    // patient who age is more 30 and they appointed to some doctors 
    [Fact]
    public void PatientsAgeMore30WithMultipleDoctors()
    {
        var bornDate = new DateTime(1995, 1, 1); // 2025 - 30 

        var patients = fixture.Appointments
            .GroupBy(app => app.Patient)
            .Where(grpbpa => grpbpa.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(grpbpa => grpbpa.Key)
            .Where(pat => pat.DateOfBirth <= bornDate)
            .OrderBy(pat => pat.DateOfBirth)
            .Select(pat => pat.FullName)
            .ToList();

        var expectedNames = new List<string>
        {
            "Никитина Ольга Петровна"
        };
        Assert.Equal(expectedNames, patients);
    }
    
    // appointments in some room at chosen month 
    [Fact]
    public void AppointmentsInRoomForChosenMonth()
    {
        const int roomNumber = 312;
        var month = new DateTime(2025, 4, 1);

        var appointments = fixture.Appointments
            .Where(app => app.RoomNumber == roomNumber &&
                          app.AppointmentDateTime.Year == month.Year &&
                          app.AppointmentDateTime.Month == month.Month)
            .Select(app => new 
            { 
                PatientName = app.Patient.FullName,
                DoctorName = app.Doctor.FullName,
                DateTime = app.AppointmentDateTime
            })            
            .ToList();

        Assert.Single(appointments);
    }
}