using Microsoft.AspNetCore.Mvc;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

/// <summary>
/// Tests for AnalyticsController
/// </summary>
public class AnalyticsControllerTests
{
    private readonly Mock<IAnalyticsService> _mockAnalyticsService;
    private readonly AnalyticsController _controller;

    public AnalyticsControllerTests()
    {
        _mockAnalyticsService = new Mock<IAnalyticsService>();
        _controller = new AnalyticsController(_mockAnalyticsService.Object);
    }

    [Fact]
    public void GetDoctorsWithExperienceMoreThan_ReturnsOkResult()
    {
        // Arrange
        var doctors = new List<DoctorDto>
        {
            new() { Id = 1, FullName = "Врач 1", Experience = 15 },
            new() { Id = 2, FullName = "Врач 2", Experience = 20 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetDoctorsWithExperienceMoreThan(10)).Returns(doctors);

        // Act
        var result = _controller.GetDoctorsWithExperienceMoreThan(10);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDoctors = Assert.IsType<List<DoctorDto>>(okResult.Value);
        Assert.Equal(2, returnedDoctors.Count);
    }

    [Fact]
    public void GetPatientsByDoctor_ReturnsOkResult()
    {
        // Arrange
        var patients = new List<PatientDto>
        {
            new() { Id = 1, FullName = "Пациент 1", Age = 30 },
            new() { Id = 2, FullName = "Пациент 2", Age = 25 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetPatientsByDoctor(1)).Returns(patients);

        // Act
        var result = _controller.GetPatientsByDoctor(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPatients = Assert.IsType<List<PatientDto>>(okResult.Value);
        Assert.Equal(2, returnedPatients.Count);
    }

    [Fact]
    public void GetRepeatAppointmentsCount_ReturnsOkResult()
    {
        // Arrange
        _mockAnalyticsService.Setup(service => service.GetRepeatAppointmentsCountForMonth(2025, 12)).Returns(5);

        // Act
        var result = _controller.GetRepeatAppointmentsCount(2025, 12);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CountResponseDto>(okResult.Value);
        Assert.Equal(5, response.Count);
    }

    [Fact]
    public void GetPatientsOverAgeWithMultipleDoctors_ReturnsOkResult()
    {
        // Arrange
        var patients = new List<PatientDto>
        {
            new() { Id = 1, FullName = "Пациент 1", Age = 40 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetPatientsOverAgeWithMultipleDoctors(30)).Returns(patients);

        // Act
        var result = _controller.GetPatientsOverAgeWithMultipleDoctors(30);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPatients = Assert.IsType<List<PatientDto>>(okResult.Value);
        Assert.Single(returnedPatients);
        Assert.Equal(40, returnedPatients[0].Age);
    }

    [Fact]
    public void GetAppointmentsInRoomForMonth_ReturnsOkResult()
    {
        // Arrange
        var appointments = new List<AppointmentDto>
        {
            new() { Id = 1, PatientName = "Пациент 1", DoctorName = "Врач 1", RoomNumber = 312 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetAppointmentsInRoomForMonth(312, 2025, 4)).Returns(appointments);

        // Act
        var result = _controller.GetAppointmentsInRoomForMonth(312, 2025, 4);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedAppointments = Assert.IsType<List<AppointmentDto>>(okResult.Value);
        Assert.Single(returnedAppointments);
        Assert.Equal(312, returnedAppointments[0].RoomNumber);
    }
}