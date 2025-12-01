using Microsoft.AspNetCore.Mvc;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

/// <summary>
/// Tests for DoctorsController
/// </summary>
public class DoctorsControllerTests
{
    private readonly Mock<IDoctorService> _mockDoctorService;
    private readonly DoctorsController _controller;

    public DoctorsControllerTests()
    {
        _mockDoctorService = new Mock<IDoctorService>();
        _controller = new DoctorsController(_mockDoctorService.Object);
    }

    [Fact]
    public void GetAllDoctors_ReturnsOkResult()
    {
        // Arrange
        var doctors = new List<DoctorDto>
        {
            new() { Id = 1, FullName = "Врач 1", Experience = 5 },
            new() { Id = 2, FullName = "Врач 2", Experience = 10 }
        };
        
        _mockDoctorService.Setup(service => service.GetAllDoctors()).Returns(doctors);

        // Act
        var result = _controller.GetAllDoctors();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDoctors = Assert.IsType<List<DoctorDto>>(okResult.Value);
        Assert.Equal(2, returnedDoctors.Count);
    }

    [Fact]
    public void GetDoctorById_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var doctor = new DoctorDto { Id = 1, FullName = "Врач", Experience = 15 };
        _mockDoctorService.Setup(service => service.GetDoctorById(1)).Returns(doctor);

        // Act
        var result = _controller.GetDoctorById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDoctor = Assert.IsType<DoctorDto>(okResult.Value);
        Assert.Equal("Врач", returnedDoctor.FullName);
    }

    [Fact]
    public void GetDoctorById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockDoctorService.Setup(service => service.GetDoctorById(999)).Returns((DoctorDto?)null);

        // Act
        var result = _controller.GetDoctorById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void CreateDoctor_ValidRequest_ReturnsCreated()
    {
        // Arrange
        var createRequest = new CreateDoctorRequest
        {
            PassportNumber = "1234 567890",
            FullName = "Новый Врач",
            SpecializationId = 1
        };

        var createdDoctor = new DoctorDto { Id = 100, FullName = "Новый Врач" };
        _mockDoctorService.Setup(service => service.CreateDoctor(createRequest)).Returns(createdDoctor);

        // Act
        var result = _controller.CreateDoctor(createRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(DoctorsController.GetDoctorById), createdResult.ActionName);
        Assert.Equal(100, ((DoctorDto)createdResult.Value!).Id);
    }
}