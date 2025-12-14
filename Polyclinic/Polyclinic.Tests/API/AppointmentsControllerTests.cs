using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

public class AppointmentsControllerTests
{
    private readonly Mock<IAppointmentService> _mockAppointmentService;
    private readonly Mock<ILogger<AppointmentsController>> _mockLogger;
    private readonly AppointmentsController _controller;

    public AppointmentsControllerTests()
    {
        _mockAppointmentService = new Mock<IAppointmentService>();
        _mockLogger = new Mock<ILogger<AppointmentsController>>();
        _controller = new AppointmentsController(_mockAppointmentService.Object, _mockLogger.Object);
    }

    [Fact]
    public void GetAllAppointments_ReturnsOkResult()
    {
        var appointments = new List<AppointmentDto>
        {
            new() { Id = 1, PatientName = "Patient 1", DoctorName = "Doctor 1" },
            new() { Id = 2, PatientName = "Patient 2", DoctorName = "Doctor 2" }
        };
        
        _mockAppointmentService.Setup(service => service.GetAllAppointments()).Returns(appointments);

        var result = _controller.GetAllAppointments();

        var okResult = Assert.IsType<ActionResult<List<AppointmentDto>>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedAppointments = Assert.IsType<List<AppointmentDto>>(actionResult.Value);
        Assert.Equal(2, returnedAppointments.Count);
    }

    [Fact]
    public void GetAppointmentById_ExistingId_ReturnsOkResult()
    {
        var appointment = new AppointmentDto { Id = 1, PatientName = "Patient", DoctorName = "Doctor" };
        _mockAppointmentService.Setup(service => service.GetAppointmentById(1)).Returns(appointment);

        var result = _controller.GetAppointmentById(1);

        var okResult = Assert.IsType<ActionResult<AppointmentDto>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedAppointment = Assert.IsType<AppointmentDto>(actionResult.Value);
        Assert.Equal("Patient", returnedAppointment.PatientName);
    }

    [Fact]
    public void GetAppointmentById_NonExistingId_ReturnsNotFound()
    {
        _mockAppointmentService.Setup(service => service.GetAppointmentById(999)).Returns((AppointmentDto?)null);

        var result = _controller.GetAppointmentById(999);

        var okResult = Assert.IsType<ActionResult<AppointmentDto>>(result);
        Assert.IsType<NotFoundObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetAppointmentById_InvalidId_ReturnsBadRequest()
    {
        var result = _controller.GetAppointmentById(0);

        var okResult = Assert.IsType<ActionResult<AppointmentDto>>(result);
        Assert.IsType<BadRequestObjectResult>(okResult.Result);
    }

    [Fact]
    public void CreateAppointment_ValidRequest_ReturnsCreated()
    {
        var createRequest = new CreateAppointmentRequest
        {
            PatientId = 1,
            DoctorId = 1,
            AppointmentDateTime = DateTime.Now.AddDays(1)
        };

        var createdAppointment = new AppointmentDto { Id = 100, PatientName = "Patient", DoctorName = "Doctor" };
        _mockAppointmentService.Setup(service => service.CreateAppointment(createRequest)).Returns(createdAppointment);

        var result = _controller.CreateAppointment(createRequest);

        var createdResult = Assert.IsType<ActionResult<AppointmentDto>>(result);
        var actionResult = Assert.IsType<CreatedAtActionResult>(createdResult.Result);
        Assert.Equal(nameof(AppointmentsController.GetAppointmentById), actionResult.ActionName);
        Assert.Equal(100, ((AppointmentDto)actionResult.Value!).Id);
    }

    [Fact]
    public void UpdateAppointment_ValidRequest_ReturnsOk()
    {
        var updateRequest = new UpdateAppointmentRequest
        {
            PatientId = 1,
            DoctorId = 1,
            AppointmentDateTime = DateTime.Now.AddDays(2)
        };

        var updatedAppointment = new AppointmentDto { Id = 1, PatientName = "Patient", DoctorName = "Doctor" };
        _mockAppointmentService.Setup(service => service.UpdateAppointment(1, updateRequest)).Returns(updatedAppointment);

        var result = _controller.UpdateAppointment(1, updateRequest);

        var okResult = Assert.IsType<ActionResult<AppointmentDto>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedAppointment = Assert.IsType<AppointmentDto>(actionResult.Value);
        Assert.Equal("Patient", returnedAppointment.PatientName);
    }

    [Fact]
    public void UpdateAppointment_NonExistingId_ReturnsNotFound()
    {
        var updateRequest = new UpdateAppointmentRequest
        {
            PatientId = 1,
            DoctorId = 1,
            AppointmentDateTime = DateTime.Now.AddDays(2)
        };

        _mockAppointmentService.Setup(service => service.UpdateAppointment(999, updateRequest)).Returns((AppointmentDto?)null);

        var result = _controller.UpdateAppointment(999, updateRequest);

        var okResult = Assert.IsType<ActionResult<AppointmentDto>>(result);
        Assert.IsType<NotFoundObjectResult>(okResult.Result);
    }

    [Fact]
    public void DeleteAppointment_ExistingId_ReturnsNoContent()
    {
        _mockAppointmentService.Setup(service => service.DeleteAppointment(1)).Returns(true);

        var result = _controller.DeleteAppointment(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteAppointment_NonExistingId_ReturnsNotFound()
    {
        _mockAppointmentService.Setup(service => service.DeleteAppointment(999)).Returns(false);

        var result = _controller.DeleteAppointment(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void DeleteAppointment_InvalidId_ReturnsBadRequest()
    {
        var result = _controller.DeleteAppointment(0);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}