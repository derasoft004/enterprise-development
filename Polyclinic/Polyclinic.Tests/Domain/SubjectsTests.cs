using Polyclinic.Domain.Enums;
using Polyclinic.Domain.Subjects;
using Xunit;

namespace Polyclinic.Tests.Domain;

/// <summary>
/// Tests for Domain Entities
/// </summary>
public class EntitiesTests
{
    [Fact]
    public void Patient_Constructor_SetsProperties()
    {
        // Arrange
        var birthDate = new DateTime(1990, 1, 1);

        // Act
        var patient = new Patient
        {
            Id = 1,
            PassportNumber = "1234 567890",
            FullName = "Иванов Иван Иванович",
            DateOfBirth = birthDate,
            Gender = Gender.Male,
            Address = "ул. Примерная, 1",
            BloodGroup = BloodGroup.A,
            ResusFactor = ResusFactor.Positive,
            PhoneNumber = "89161234567"
        };

        // Assert
        Assert.Equal(1, patient.Id);
        Assert.Equal("1234 567890", patient.PassportNumber);
        Assert.Equal("Иванов Иван Иванович", patient.FullName);
        Assert.Equal(birthDate, patient.DateOfBirth);
        Assert.Equal(Gender.Male, patient.Gender);
        Assert.Equal("ул. Примерная, 1", patient.Address);
        Assert.Equal(BloodGroup.A, patient.BloodGroup);
        Assert.Equal(ResusFactor.Positive, patient.ResusFactor);
        Assert.Equal("89161234567", patient.PhoneNumber);
    }

    [Fact]
    public void Doctor_Constructor_SetsProperties()
    {
        // Arrange
        var specialization = new Specialization { Id = 1, Name = "Терапевт" };

        // Act
        var doctor = new Doctor
        {
            Id = 1,
            PassportNumber = "4321 987654",
            FullName = "Петров Петр Петрович",
            YearOfBirth = 1980,
            Specialization = specialization,
            Experience = 10
        };

        // Assert
        Assert.Equal(1, doctor.Id);
        Assert.Equal("4321 987654", doctor.PassportNumber);
        Assert.Equal("Петров Петр Петрович", doctor.FullName);
        Assert.Equal(1980, doctor.YearOfBirth);
        Assert.Equal(specialization, doctor.Specialization);
        Assert.Equal(10, doctor.Experience);
    }

    [Fact]
    public void Appointment_Constructor_SetsProperties()
    {
        // Arrange
        var patient = new Patient { Id = 1, FullName = "Пациент" };
        var doctor = new Doctor { Id = 1, FullName = "Врач" };
        var appointmentDate = new DateTime(2025, 1, 20, 10, 0, 0);

        // Act
        var appointment = new Appointment
        {
            Id = 1,
            Patient = patient,
            Doctor = doctor,
            AppointmentDateTime = appointmentDate,
            RoomNumber = 101,
            RepeatAppointment = true
        };

        // Assert
        Assert.Equal(1, appointment.Id);
        Assert.Equal(patient, appointment.Patient);
        Assert.Equal(doctor, appointment.Doctor);
        Assert.Equal(appointmentDate, appointment.AppointmentDateTime);
        Assert.Equal(101, appointment.RoomNumber);
        Assert.True(appointment.RepeatAppointment);
    }
}