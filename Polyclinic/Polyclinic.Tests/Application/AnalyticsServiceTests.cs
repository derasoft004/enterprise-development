using Moq;
using Polyclinic.Application.Interfaces;
using Polyclinic.Application.Services;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Xunit;

namespace Polyclinic.Tests.Application;

/// <summary>
/// Tests for AnalyticsService
/// </summary>
public class AnalyticsServiceTests
{
    private readonly Mock<IRepository<Appointment, int>> _mockAppointmentRepository;
    private readonly Mock<IRepository<Patient, int>> _mockPatientRepository;
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsServiceTests()
    {
        var mockDoctorRepository = new Mock<IRepository<Doctor, int>>();
        _mockAppointmentRepository = new Mock<IRepository<Appointment, int>>();
        _mockPatientRepository = new Mock<IRepository<Patient, int>>();
        _analyticsService = new AnalyticsService(
            mockDoctorRepository.Object,
            _mockAppointmentRepository.Object);
    }

    [Fact]
    public void GetPatientsByDoctor_ReturnsCorrectPatients()
    {
        // Arrange
        var patient1 = new Patient { Id = 1, FullName = "Пациент 1" };
        var patient2 = new Patient { Id = 2, FullName = "Пациент 2" };
        var doctor = new Doctor { Id = 1, FullName = "Врач" };
        
        var appointments = new List<Appointment>
        {
            new() { Id = 1, Patient = patient1, Doctor = doctor },
            new() { Id = 2, Patient = patient2, Doctor = doctor },
            new() { Id = 3, Patient = patient1, Doctor = doctor } // Дубликат пациента 1
        };

        _mockAppointmentRepository.Setup(repo => repo.ReadAll()).Returns(appointments);

        // Act
        var result = _analyticsService.GetPatientsByDoctor(1);

        // Assert
        Assert.Equal(2, result.Count); // Должны быть 2 уникальных пациента
        Assert.Contains(result, p => p.FullName == "Пациент 1");
        Assert.Contains(result, p => p.FullName == "Пациент 2");
    }

    [Fact]
    public void GetPatientsOverAgeWithMultipleDoctors_ReturnsCorrectPatients()
    {
        // Arrange
        var patient1 = new Patient { Id = 1, FullName = "Пациент 1", DateOfBirth = new DateTime(1980, 1, 1) }; // 45 лет
        var patient2 = new Patient { Id = 2, FullName = "Пациент 2", DateOfBirth = new DateTime(2010, 1, 1) }; // 15 лет
        
        var doctor1 = new Doctor { Id = 1, FullName = "Врач 1" };
        var doctor2 = new Doctor { Id = 2, FullName = "Врач 2" };
        
        var appointments = new List<Appointment>
        {
            new() { Patient = patient1, Doctor = doctor1 },
            new() { Patient = patient1, Doctor = doctor2 }, // Пациент 1 у 2 врачей
            new() { Patient = patient2, Doctor = doctor1 },
            new() { Patient = patient2, Doctor = doctor2 }  // Пациент 2 у 2 врачей, но молодой
        };

        _mockAppointmentRepository.Setup(repo => repo.ReadAll()).Returns(appointments);

        // Act
        var result = _analyticsService.GetPatientsOverAgeWithMultipleDoctors(30);

        // Assert
        Assert.Single(result); // Только пациент 1 (старше 30 лет с несколькими врачами)
        Assert.Equal("Пациент 1", result[0].FullName);
    }
}