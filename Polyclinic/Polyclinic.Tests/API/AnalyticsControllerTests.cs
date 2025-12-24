using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

public class AnalyticsControllerTests
{
    private readonly Mock<IAnalyticsService> _mockAnalyticsService;
    private readonly Mock<ILogger<AnalyticsController>> _mockLogger;
    private readonly AnalyticsController _controller;

    public AnalyticsControllerTests()
    {
        _mockAnalyticsService = new Mock<IAnalyticsService>();
        _mockLogger = new Mock<ILogger<AnalyticsController>>();
        _controller = new AnalyticsController(_mockAnalyticsService.Object, _mockLogger.Object);
    }

    [Fact]
    public void GetDoctorsWithExperienceMoreThan_ReturnsOkResult()
    {
        var doctors = new List<DoctorDto>
        {
            new() { Id = 1, FullName = "Doctor 1", Experience = 15 },
            new() { Id = 2, FullName = "Doctor 2", Experience = 20 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetDoctorsWithExperienceMoreThan(10)).Returns(doctors);

        var result = _controller.GetDoctorsWithExperienceMoreThan(10);

        var okResult = Assert.IsType<ActionResult<List<DoctorDto>>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedDoctors = Assert.IsType<List<DoctorDto>>(actionResult.Value);
        Assert.Equal(2, returnedDoctors.Count);
    }

    [Fact]
    public void GetPatientsByDoctor_ReturnsOkResult()
    {
        var patients = new List<PatientDto>
        {
            new() { Id = 1, FullName = "Patient 1", Age = 30 },
            new() { Id = 2, FullName = "Patient 2", Age = 25 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetPatientsByDoctor(1)).Returns(patients);

        var result = _controller.GetPatientsByDoctor(1);

        var okResult = Assert.IsType<ActionResult<List<PatientDto>>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedPatients = Assert.IsType<List<PatientDto>>(actionResult.Value);
        Assert.Equal(2, returnedPatients.Count);
    }

    [Fact]
    public void GetRepeatAppointmentsByMonth_ReturnsOkResult()
    {
        _mockAnalyticsService.Setup(service => service.GetRepeatAppointmentsCountForMonth(2025, 12)).Returns(5);

        var result = _controller.GetRepeatAppointmentsByMonth(2025, 12);

        var okResult = Assert.IsType<ActionResult<int>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var count = Assert.IsType<int>(actionResult.Value);
        Assert.Equal(5, count);
    }

    [Fact]
    public void GetPatientsOverAgeWithMultipleDoctors_ReturnsOkResult()
    {
        var patients = new List<PatientDto>
        {
            new() { Id = 1, FullName = "Patient 1", Age = 40 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetPatientsOverAgeWithMultipleDoctors(30)).Returns(patients);

        var result = _controller.GetPatientsOlderThanWithMultipleDoctors(30);

        var okResult = Assert.IsType<ActionResult<List<PatientDto>>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedPatients = Assert.IsType<List<PatientDto>>(actionResult.Value);
        Assert.Single(returnedPatients);
        Assert.Equal(40, returnedPatients[0].Age);
    }

    [Fact]
    public void GetAppointmentsInRoomForMonth_ReturnsOkResult()
    {
        var appointments = new List<AppointmentDto>
        {
            new() { Id = 1, PatientName = "Patient 1", DoctorName = "Doctor 1", RoomNumber = 312 }
        };
        
        _mockAnalyticsService.Setup(service => service.GetAppointmentsInRoomForMonth(312, 2025, 4)).Returns(appointments);

        var result = _controller.GetAppointmentsByRoomAndMonth(312, 2025, 4);

        var okResult = Assert.IsType<ActionResult<List<AppointmentDto>>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedAppointments = Assert.IsType<List<AppointmentDto>>(actionResult.Value);
        Assert.Single(returnedAppointments);
        Assert.Equal(312, returnedAppointments[0].RoomNumber);
    }

    [Fact]
    public void GetRepeatAppointmentsByMonth_InvalidYear_ReturnsBadRequest()
    {
        var result = _controller.GetRepeatAppointmentsByMonth(1999, 12);

        var okResult = Assert.IsType<ActionResult<int>>(result);
        Assert.IsType<BadRequestObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetRepeatAppointmentsByMonth_InvalidMonth_ReturnsBadRequest()
    {
        var result = _controller.GetRepeatAppointmentsByMonth(2025, 13);

        var okResult = Assert.IsType<ActionResult<int>>(result);
        Assert.IsType<BadRequestObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetDoctorsWithExperienceMoreThan_NegativeYears_ReturnsBadRequest()
    {
        var result = _controller.GetDoctorsWithExperienceMoreThan(-1);

        var okResult = Assert.IsType<ActionResult<List<DoctorDto>>>(result);
        Assert.IsType<BadRequestObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetPatientsByDoctor_InvalidId_ReturnsBadRequest()
    {
        var result = _controller.GetPatientsByDoctor(0);

        var okResult = Assert.IsType<ActionResult<List<PatientDto>>>(result);
        Assert.IsType<BadRequestObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetPatientsByDoctor_NoPatients_ReturnsNotFound()
    {
        _mockAnalyticsService.Setup(service => service.GetPatientsByDoctor(999)).Returns([]);

        var result = _controller.GetPatientsByDoctor(999);

        var okResult = Assert.IsType<ActionResult<List<PatientDto>>>(result);
        Assert.IsType<NotFoundObjectResult>(okResult.Result);
    }
}