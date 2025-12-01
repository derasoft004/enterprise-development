using Microsoft.AspNetCore.Mvc;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

/// <summary>
/// Tests for AppointmentsController
/// </summary>
public class AppointmentsControllerTests
{
    private readonly Mock<IAppointmentService> _mockAppointmentService;
    private readonly AppointmentsController _controller;

    public AppointmentsControllerTests()
    {
        _mockAppointmentService = new Mock<IAppointmentService>();
        _controller = new AppointmentsController(_mockAppointmentService.Object);
    }

    [Fact]
    public void GetAllAppointments_ReturnsOkResult()
    {
        // Arrange
        var appointments = new List<AppointmentDto>
        {
            new() { Id = 1, PatientName = "Пациент 1", DoctorName = "Врач 1" },
            new() { Id = 2, PatientName = "Пациент 2", DoctorName = "Врач 2" }
        };
        
        _mockAppointmentService.Setup(service => service.GetAllAppointments()).Returns(appointments);

        // Act
        var result = _controller.GetAllAppointments();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedAppointments = Assert.IsType<List<AppointmentDto>>(okResult.Value);
        Assert.Equal(2, returnedAppointments.Count);
    }

    [Fact]
    public void GetAppointmentsByDoctor_ReturnsOkResult()
    {
        // Arrange
        var appointments = new List<AppointmentDto>
        {
            new() { Id = 1, PatientName = "Пациент 1", DoctorName = "Врач 1" },
            new() { Id = 2, PatientName = "Пациент 2", DoctorName = "Врач 1" }
        };
        
        _mockAppointmentService.Setup(service => service.GetAppointmentsByDoctor(1)).Returns(appointments);

        // Act
        var result = _controller.GetAppointmentsByDoctor(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedAppointments = Assert.IsType<List<AppointmentDto>>(okResult.Value);
        Assert.Equal(2, returnedAppointments.Count);
        Assert.All(returnedAppointments, a => Assert.Equal("Врач 1", a.DoctorName));
    }
}