using Moq;
using Polyclinic.Application.Interfaces;
using Polyclinic.Application.Services;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Xunit;

namespace Polyclinic.Tests.Application;

/// <summary>
/// Tests for AppointmentService
/// </summary>
public class AppointmentServiceTests
{
    private readonly Mock<IRepository<Appointment, int>> _mockAppointmentRepository;
    private readonly AppointmentService _appointmentService;

    public AppointmentServiceTests()
    {
        _mockAppointmentRepository = new Mock<IRepository<Appointment, int>>();
        var mockPatientRepository = new Mock<IRepository<Patient, int>>();
        var mockDoctorRepository = new Mock<IRepository<Doctor, int>>();
        _appointmentService = new AppointmentService(
            _mockAppointmentRepository.Object,
            mockPatientRepository.Object,
            mockDoctorRepository.Object);
    }

    [Fact]
    public void GetAllAppointments_ReturnsAppointments()
    {
        // Arrange
        var patient = new Patient { Id = 1, FullName = "Пациент" };
        var doctor = new Doctor { Id = 1, FullName = "Врач" };
        var appointments = new List<Appointment>
        {
            new() { Id = 1, Patient = patient, Doctor = doctor, AppointmentDateTime = DateTime.Now },
            new() { Id = 2, Patient = patient, Doctor = doctor, AppointmentDateTime = DateTime.Now.AddDays(1) }
        };

        _mockAppointmentRepository.Setup(repo => repo.ReadAll()).Returns(appointments);

        // Act
        var result = _appointmentService.GetAllAppointments();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Пациент", result[0].PatientName);
        Assert.Equal("Врач", result[0].DoctorName);
    }

    [Fact]
    public void GetAppointmentsByDoctor_ReturnsDoctorAppointments()
    {
        // Arrange
        var patient = new Patient { Id = 1, FullName = "Пациент" };
        var doctor1 = new Doctor { Id = 1, FullName = "Врач 1" };
        var doctor2 = new Doctor { Id = 2, FullName = "Врач 2" };
        
        var appointments = new List<Appointment>
        {
            new() { Id = 1, Patient = patient, Doctor = doctor1 },
            new() { Id = 2, Patient = patient, Doctor = doctor1 },
            new() { Id = 3, Patient = patient, Doctor = doctor2 }
        };

        _mockAppointmentRepository.Setup(repo => repo.ReadAll()).Returns(appointments);

        // Act
        var result = _appointmentService.GetAppointmentsByDoctor(1);

        // Assert
        Assert.Equal(2, result.Count); // Только записи к врачу 1
        Assert.All(result, a => Assert.Equal("Врач 1", a.DoctorName));
    }

    [Fact]
    public void GetRepeatAppointmentsCountForMonth_ReturnsCorrectCount()
    {
        // Arrange
        var patient = new Patient { Id = 1, FullName = "Пациент" };
        var doctor = new Doctor { Id = 1, FullName = "Врач" };
        
        var appointments = new List<Appointment>
        {
            new() { Id = 1, Patient = patient, Doctor = doctor, 
                   AppointmentDateTime = new DateTime(2025, 12, 10), RepeatAppointment = true },
            new() { Id = 2, Patient = patient, Doctor = doctor, 
                   AppointmentDateTime = new DateTime(2025, 12, 15), RepeatAppointment = true },
            new() { Id = 3, Patient = patient, Doctor = doctor, 
                   AppointmentDateTime = new DateTime(2025, 11, 10), RepeatAppointment = true } // Другой месяц
        };

        _mockAppointmentRepository.Setup(repo => repo.ReadAll()).Returns(appointments);

        // Act
        var result = _appointmentService.GetRepeatAppointmentsCountForMonth(2025, 12);

        // Assert
        Assert.Equal(2, result); // Только 2 повторные записи за декабрь 2025
    }
}