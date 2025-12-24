using Moq;
using Polyclinic.Application.Interfaces;
using Polyclinic.Application.Services;
using Polyclinic.Contracts.Dto;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;
using Xunit;

namespace Polyclinic.Tests.Application;

/// <summary>
/// Tests for DoctorService
/// </summary>
public class DoctorServiceTests
{
    private readonly Mock<IRepository<Doctor, int>> _mockDoctorRepository;
    private readonly DoctorService _doctorService;

    public DoctorServiceTests()
    {
        _mockDoctorRepository = new Mock<IRepository<Doctor, int>>();
        var mockSpecializationRepository = new Mock<IRepository<Specialization, int>>();
        _doctorService = new DoctorService(
            _mockDoctorRepository.Object,
            mockSpecializationRepository.Object);
    }

    [Fact]
    public void GetAllDoctors_ReturnsDoctors()
    {
        // Arrange
        var specialization = new Specialization { Id = 1, Name = "Терапевт" };
        var doctors = new List<Doctor>
        {
            new() { Id = 1, FullName = "Доктор 1", Specialization = specialization, Experience = 5 },
            new() { Id = 2, FullName = "Доктор 2", Specialization = specialization, Experience = 10 }
        };

        _mockDoctorRepository.Setup(repo => repo.ReadAll()).Returns(doctors);

        // Act
        var result = _doctorService.GetAllDoctors();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Доктор 1", result[0].FullName);
        Assert.Equal("Доктор 2", result[1].FullName);
        Assert.Equal("Терапевт", result[0].Specialization);
    }

    [Fact]
    public void GetDoctorById_ExistingId_ReturnsDoctor()
    {
        // Arrange
        var specialization = new Specialization { Id = 1, Name = "Хирург" };
        var doctor = new Doctor { Id = 1, FullName = "Хирург", Specialization = specialization, Experience = 15 };
        _mockDoctorRepository.Setup(repo => repo.Read(1)).Returns(doctor);

        // Act
        var result = _doctorService.GetDoctorById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Хирург", result.FullName);
        Assert.Equal("Senior", result.ExperienceLevel); // 15 лет = Senior
    }

    [Fact]
    public void GetDoctorsWithExperienceMoreThan_ReturnsFilteredDoctors()
    {
        // Arrange
        var specialization = new Specialization { Id = 1, Name = "Терапевт" };
        var doctors = new List<Doctor>
        {
            new() { Id = 1, FullName = "Доктор 1", Specialization = specialization, Experience = 5 },
            new() { Id = 2, FullName = "Доктор 2", Specialization = specialization, Experience = 10 },
            new() { Id = 3, FullName = "Доктор 3", Specialization = specialization, Experience = 15 }
        };

        _mockDoctorRepository.Setup(repo => repo.ReadAll()).Returns(doctors);

        // Act
        var result = _doctorService.GetDoctorsWithExperienceMoreThan(10);

        // Assert
        Assert.Equal(2, result.Count); // Должны вернуться доктора с опытом 10 и 15 лет
        Assert.All(result, d => Assert.True(d.Experience >= 10));
    }
}