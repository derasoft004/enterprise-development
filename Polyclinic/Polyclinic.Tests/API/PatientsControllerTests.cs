using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

public class PatientsControllerTests
{
    private readonly Mock<IPatientService> _mockPatientService;
    private readonly Mock<ILogger<PatientsController>> _mockLogger;
    private readonly PatientsController _controller;

    public PatientsControllerTests()
    {
        _mockPatientService = new Mock<IPatientService>();
        _mockLogger = new Mock<ILogger<PatientsController>>();
        _controller = new PatientsController(_mockPatientService.Object, _mockLogger.Object);
    }

    [Fact]
    public void GetAllPatients_ReturnsOkResult()
    {
        var patients = new List<PatientDto>
        {
            new() { Id = 1, FullName = "Patient 1", Age = 30 },
            new() { Id = 2, FullName = "Patient 2", Age = 25 }
        };
        
        _mockPatientService.Setup(service => service.GetAllPatients()).Returns(patients);

        var result = _controller.GetAllPatients();

        var okResult = Assert.IsType<ActionResult<List<PatientDto>>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedPatients = Assert.IsType<List<PatientDto>>(actionResult.Value);
        Assert.Equal(2, returnedPatients.Count);
    }

    [Fact]
    public void GetPatientById_ExistingId_ReturnsOkResult()
    {
        var patient = new PatientDto { Id = 1, FullName = "Patient", Age = 30 };
        _mockPatientService.Setup(service => service.GetPatientById(1)).Returns(patient);

        var result = _controller.GetPatientById(1);

        var okResult = Assert.IsType<ActionResult<PatientDto>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedPatient = Assert.IsType<PatientDto>(actionResult.Value);
        Assert.Equal("Patient", returnedPatient.FullName);
    }

    [Fact]
    public void GetPatientById_NonExistingId_ReturnsNotFound()
    {
        _mockPatientService.Setup(service => service.GetPatientById(999)).Returns((PatientDto?)null);

        var result = _controller.GetPatientById(999);

        var okResult = Assert.IsType<ActionResult<PatientDto>>(result);
        Assert.IsType<NotFoundObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetPatientById_InvalidId_ReturnsBadRequest()
    {
        var result = _controller.GetPatientById(0);

        var okResult = Assert.IsType<ActionResult<PatientDto>>(result);
        Assert.IsType<BadRequestObjectResult>(okResult.Result);
    }

    [Fact]
    public void CreatePatient_ValidRequest_ReturnsCreated()
    {
        var createRequest = new CreatePatientRequest
        {
            PassportNumber = "1234567890",
            FullName = "New Patient",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        var createdPatient = new PatientDto { Id = 100, FullName = "New Patient" };
        _mockPatientService.Setup(service => service.CreatePatient(createRequest)).Returns(createdPatient);

        var result = _controller.CreatePatient(createRequest);

        var createdResult = Assert.IsType<ActionResult<PatientDto>>(result);
        var actionResult = Assert.IsType<CreatedAtActionResult>(createdResult.Result);
        Assert.Equal(nameof(PatientsController.GetPatientById), actionResult.ActionName);
        Assert.Equal(100, ((PatientDto)actionResult.Value!).Id);
    }

    [Fact]
    public void UpdatePatient_ValidRequest_ReturnsOk()
    {
        var updateRequest = new UpdatePatientRequest
        {
            FullName = "Updated Patient"
        };

        var updatedPatient = new PatientDto { Id = 1, FullName = "Updated Patient" };
        _mockPatientService.Setup(service => service.UpdatePatient(1, updateRequest)).Returns(updatedPatient);

        var result = _controller.UpdatePatient(1, updateRequest);

        var okResult = Assert.IsType<ActionResult<PatientDto>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedPatient = Assert.IsType<PatientDto>(actionResult.Value);
        Assert.Equal("Updated Patient", returnedPatient.FullName);
    }

    [Fact]
    public void UpdatePatient_NonExistingId_ReturnsNotFound()
    {
        var updateRequest = new UpdatePatientRequest
        {
            FullName = "Updated Patient"
        };

        _mockPatientService.Setup(service => service.UpdatePatient(999, updateRequest)).Returns((PatientDto?)null);

        var result = _controller.UpdatePatient(999, updateRequest);

        var okResult = Assert.IsType<ActionResult<PatientDto>>(result);
        Assert.IsType<NotFoundObjectResult>(okResult.Result);
    }

    [Fact]
    public void DeletePatient_ExistingId_ReturnsNoContent()
    {
        _mockPatientService.Setup(service => service.DeletePatient(1)).Returns(true);

        var result = _controller.DeletePatient(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeletePatient_NonExistingId_ReturnsNotFound()
    {
        _mockPatientService.Setup(service => service.DeletePatient(999)).Returns(false);

        var result = _controller.DeletePatient(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void DeletePatient_InvalidId_ReturnsBadRequest()
    {
        var result = _controller.DeletePatient(0);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}