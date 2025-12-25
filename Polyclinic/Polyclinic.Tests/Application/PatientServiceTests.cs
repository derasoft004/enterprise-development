using Moq;
using Polyclinic.Application.Interfaces;
using Polyclinic.Application.Services;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Polyclinic.Infrastructure.PostgreSql;

namespace Polyclinic.Tests.Application;

/// <summary>
/// Tests for PatientService
/// </summary>
public class PatientServiceTests
{
    private readonly Mock<IRepository<Patient, int>> _mockPatientRepository;
    private readonly Mock<IRepository<Appointment, int>> _mockAppointmentRepository;
    private readonly PolyclinicDbContext _dbContext;
    private readonly PatientService _patientService;

    public PatientServiceTests()
    {
        _mockPatientRepository = new Mock<IRepository<Patient, int>>();
        _mockAppointmentRepository = new Mock<IRepository<Appointment, int>>();

        // InMemory DbContext 
        var options = new DbContextOptionsBuilder<PolyclinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new PolyclinicDbContext(options);

        _patientService = new PatientService(
            _mockPatientRepository.Object,
            _mockAppointmentRepository.Object);
    }

    [Fact]
    public void GetAllPatients_ReturnsPatients()
    {
        // Arrange
        var patients = new List<Patient>
        {
            new() { Id = 1, FullName = "Пациент 1", DateOfBirth = new DateTime(1990, 1, 1) },
            new() { Id = 2, FullName = "Пациент 2", DateOfBirth = new DateTime(1985, 1, 1) }
        };

        _mockPatientRepository.Setup(repo => repo.ReadAll()).Returns(patients);

        // Act
        var result = _patientService.GetAllPatients();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Пациент 1", result[0].FullName);
        Assert.Equal("Пациент 2", result[1].FullName);
    }

    [Fact]
    public void GetPatientById_ExistingId_ReturnsPatient()
    {
        // Arrange
        var patient = new Patient { Id = 1, FullName = "Пациент", DateOfBirth = new DateTime(1990, 1, 1) };
        _mockPatientRepository.Setup(repo => repo.Read(1)).Returns(patient);

        // Act
        var result = _patientService.GetPatientById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Пациент", result.FullName);
    }

    [Fact]
    public void GetPatientById_NonExistingId_ReturnsNull()
    {
        // Arrange
        _mockPatientRepository.Setup(repo => repo.Read(999)).Returns((Patient?)null);

        // Act
        var result = _patientService.GetPatientById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CreatePatient_AddsPatient()
    {
        // Arrange
        var request = new Polyclinic.Contracts.Dto.CreatePatientRequest
        {
            PassportNumber = "1234 567890",
            FullName = "Новый Пациент",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        Patient? createdPatient = null;
        _mockPatientRepository.Setup(repo => repo.Create(It.IsAny<Patient>()))
            .Callback<Patient>(p => createdPatient = p)
            .Returns(() => createdPatient!.Id);

        // Act
        var result = _patientService.CreatePatient(request);

        // Assert
        Assert.NotNull(createdPatient);
        Assert.Equal("Новый Пациент", result.FullName);
        Assert.Equal(createdPatient.Id, result.Id);
    }
}
