using Polyclinic.Domain.Subjects;

namespace Polyclinic.Tests;

public class TestFixture
{
    public List<Doctor> Doctors { get; }
    public List<Patient> Patients { get; }
    public List<Appointment> Appointments { get; }

    public TestFixture()
    {
        Doctors = DataSeed.Doctors;
        Patients = DataSeed.Patients;
        Appointments = DataSeed.Appointments;
    }
}