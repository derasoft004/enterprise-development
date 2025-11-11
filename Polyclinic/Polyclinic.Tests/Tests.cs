using Xunit;

namespace Polyclinic.Tests;

/// <summary>
///  PolyclinicTests module with unit-tests on data from dataseed 
/// </summary>
public class PolyclinicTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    /// <summary>
    /// Doctors with experience more 10 years
    /// </summary>
    [Fact]
    public void DoctorsExperienceMore10()
    {
        List<int> expectedId = [1, 2, 3, 5, 6];

        var experiencedId = fixture.Doctors
            .Where(doc => doc.Experience >= 10)
            .Select(doc => doc.Id)
            .Order()
            .ToList();

        Assert.Equal(expectedId, experiencedId);
    }
    
    /// <summary>
    /// Patients by appointments to some doctors by doctor's id
    /// </summary>
    [Fact]
    public void PatientsByDoctor()
    {
        const int doctorId = 6;
        var expectedNames = new List<string> 
        {
            "Александров Александр Александрович", 
            "Макарова Виктория Андреевна"
        };
        
        var result = fixture.Appointments
            .Where(app => app.Doctor.Id == doctorId)
            .Select(app => app.Patient.FullName)
            .OrderBy(pat => pat)
            .ToList();
        
        Assert.Equal(expectedNames, result);
    }
    
    /// <summary>
    /// Repeated appointments at last month 
    /// </summary>
    [Fact]
    public void RepeatAppointmentsCountLastMonth()
    {
        var month = new DateTime(2025, 12, 1);
        const int expectedCount = 2; // 2 true and 1 false

        var result = fixture.Appointments.Count(app =>
            app.RepeatAppointment &&
            app.AppointmentDateTime.Year == month.Year &&
            app.AppointmentDateTime.Month == month.Month);
        
        Assert.Equal(expectedCount, result);
    }
    
    /// <summary>
    /// Patient who age is more 30 and they appointed to some doctors 
    /// </summary>
    [Fact]
    public void PatientsAgeMore30WithMultipleDoctors()
    {
        var bornDate = new DateTime(1995, 1, 1); // 2025 - 30 
        var expectedNames = new List<string>
        {
            "Никитина Ольга Петровна"
        };
        
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
    
    /// <summary>
    /// Appointments in some room at chosen month 
    /// </summary>
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