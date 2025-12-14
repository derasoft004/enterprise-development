using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Polyclinic.API.Host.Controllers;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

public class DoctorsControllerTests
{
    private readonly Mock<IDoctorService> _mockDoctorService;
    private readonly DoctorsController _controller;

    public DoctorsControllerTests()
    {
        _mockDoctorService = new Mock<IDoctorService>();
        var mockLogger = new Mock<ILogger<DoctorsController>>();
        _controller = new DoctorsController(_mockDoctorService.Object, mockLogger.Object);
    }

    [Fact]
    public void GetAllDoctors_ReturnsOkResult()
    {
        var doctors = new List<DoctorDto>
        {
            new() { Id = 1, FullName = "Doctor 1", Experience = 5 },
            new() { Id = 2, FullName = "Doctor 2", Experience = 10 }
        };
        
        _mockDoctorService.Setup(service => service.GetAllDoctors()).Returns(doctors);

        var result = _controller.GetAllDoctors();

        var okResult = Assert.IsType<ActionResult<List<DoctorDto>>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedDoctors = Assert.IsType<List<DoctorDto>>(actionResult.Value);
        Assert.Equal(2, returnedDoctors.Count);
    }

    [Fact]
    public void GetDoctorById_ExistingId_ReturnsOkResult()
    {
        var doctor = new DoctorDto { Id = 1, FullName = "Doctor", Experience = 15 };
        _mockDoctorService.Setup(service => service.GetDoctorById(1)).Returns(doctor);

        var result = _controller.GetDoctorById(1);

        var okResult = Assert.IsType<ActionResult<DoctorDto>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedDoctor = Assert.IsType<DoctorDto>(actionResult.Value);
        Assert.Equal("Doctor", returnedDoctor.FullName);
    }

    [Fact]
    public void GetDoctorById_NonExistingId_ReturnsNotFound()
    {
        _mockDoctorService.Setup(service => service.GetDoctorById(999)).Returns((DoctorDto?)null);

        var result = _controller.GetDoctorById(999);

        var okResult = Assert.IsType<ActionResult<DoctorDto>>(result);
        Assert.IsType<NotFoundObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetDoctorById_InvalidId_ReturnsBadRequest()
    {
        var result = _controller.GetDoctorById(0);

        var okResult = Assert.IsType<ActionResult<DoctorDto>>(result);
        Assert.IsType<BadRequestObjectResult>(okResult.Result);
    }

    [Fact]
    public void CreateDoctor_ValidRequest_ReturnsCreated()
    {
        var createRequest = new CreateDoctorRequest
        {
            PassportNumber = "1234567890",
            FullName = "New Doctor",
            SpecializationId = 1
        };

        var createdDoctor = new DoctorDto { Id = 100, FullName = "New Doctor" };
        _mockDoctorService.Setup(service => service.CreateDoctor(createRequest)).Returns(createdDoctor);

        var result = _controller.CreateDoctor(createRequest);

        var createdResult = Assert.IsType<ActionResult<DoctorDto>>(result);
        var actionResult = Assert.IsType<CreatedAtActionResult>(createdResult.Result);
        Assert.Equal(nameof(DoctorsController.GetDoctorById), actionResult.ActionName);
        Assert.Equal(100, ((DoctorDto)actionResult.Value!).Id);
    }

    [Fact]
    public void UpdateDoctor_ValidRequest_ReturnsOk()
    {
        var updateRequest = new UpdateDoctorRequest
        {
            FullName = "Updated Doctor",
            SpecializationId = 2
        };

        var updatedDoctor = new DoctorDto { Id = 1, FullName = "Updated Doctor" };
        _mockDoctorService.Setup(service => service.UpdateDoctor(1, updateRequest)).Returns(updatedDoctor);

        var result = _controller.UpdateDoctor(1, updateRequest);

        var okResult = Assert.IsType<ActionResult<DoctorDto>>(result);
        var actionResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var returnedDoctor = Assert.IsType<DoctorDto>(actionResult.Value);
        Assert.Equal("Updated Doctor", returnedDoctor.FullName);
    }

    [Fact]
    public void UpdateDoctor_NonExistingId_ReturnsNotFound()
    {
        var updateRequest = new UpdateDoctorRequest
        {
            FullName = "Updated Doctor",
            SpecializationId = 2
        };

        _mockDoctorService.Setup(service => service.UpdateDoctor(999, updateRequest)).Returns((DoctorDto?)null);

        var result = _controller.UpdateDoctor(999, updateRequest);

        var okResult = Assert.IsType<ActionResult<DoctorDto>>(result);
        Assert.IsType<NotFoundObjectResult>(okResult.Result);
    }

    [Fact]
    public void DeleteDoctor_ExistingId_ReturnsNoContent()
    {
        _mockDoctorService.Setup(service => service.DeleteDoctor(1)).Returns(true);

        var result = _controller.DeleteDoctor(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteDoctor_NonExistingId_ReturnsNotFound()
    {
        _mockDoctorService.Setup(service => service.DeleteDoctor(999)).Returns(false);

        var result = _controller.DeleteDoctor(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void DeleteDoctor_InvalidId_ReturnsBadRequest()
    {
        var result = _controller.DeleteDoctor(0);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}