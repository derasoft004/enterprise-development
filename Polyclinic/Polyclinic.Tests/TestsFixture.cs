using Polyclinic.Domain.Subjects;

namespace Polyclinic.Tests;

/// <summary>
/// Class TestFixture using to make List from Doctors, Patients or Appointments and use it in tests
/// </summary>
public class TestFixture
{
    /// <summary>
    /// List of Doctors for tests
    /// </summary>
    public List<Doctor> Doctors { get; }
    
    /// <summary>
    /// List of Patients for tests
    /// </summary>
    public List<Patient> Patients { get; }
    
    /// <summary>
    /// List of Appointments for tests
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// TestFixture constructor that fill fields Doctors, Patients or Appointments from DataSeed
    /// </summary>
    public TestFixture()
    {
        Doctors = DataSeed.Doctors;
        Patients = DataSeed.Patients;
        Appointments = DataSeed.Appointments;
    }
}