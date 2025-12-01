using Microsoft.AspNetCore.Mvc;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

/// <summary>
/// Tests for PatientsController
/// </summary>
public class PatientsControllerTests
{
    private readonly Mock<IPatientService> _mockPatientService;
    private readonly PatientsController _controller;

    public PatientsControllerTests()
    {
        _mockPatientService = new Mock<IPatientService>();
        _controller = new PatientsController(_mockPatientService.Object);
    }

    [Fact]
    public void GetAllPatients_ReturnsOkResult()
    {
        // Arrange
        var patients = new List<PatientDto>
        {
            new() { Id = 1, FullName = "Пациент 1", Age = 30 },
            new() { Id = 2, FullName = "Пациент 2", Age = 25 }
        };
        
        _mockPatientService.Setup(service => service.GetAllPatients()).Returns(patients);

        // Act
        var result = _controller.GetAllPatients();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPatients = Assert.IsType<List<PatientDto>>(okResult.Value);
        Assert.Equal(2, returnedPatients.Count);
    }

    [Fact]
    public void GetPatientById_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var patient = new PatientDto { Id = 1, FullName = "Пациент", Age = 30 };
        _mockPatientService.Setup(service => service.GetPatientById(1)).Returns(patient);

        // Act
        var result = _controller.GetPatientById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
        Assert.Equal("Пациент", returnedPatient.FullName);
    }

    [Fact]
    public void GetPatientById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockPatientService.Setup(service => service.GetPatientById(999)).Returns((PatientDto?)null);

        // Act
        var result = _controller.GetPatientById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}