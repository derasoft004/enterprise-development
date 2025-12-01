using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Get doctors with experience more than specified years
    /// </summary>
    [HttpGet("doctors/experience/{years}")]
    public IActionResult GetDoctorsWithExperienceMoreThan(int years)
    {
        var doctors = _analyticsService.GetDoctorsWithExperienceMoreThan(years);
        return Ok(doctors);
    }

    /// <summary>
    /// Get patients by doctor ID
    /// </summary>
    [HttpGet("doctors/{doctorId}/patients")]
    public IActionResult GetPatientsByDoctor(int doctorId)
    {
        var patients = _analyticsService.GetPatientsByDoctor(doctorId);
        return Ok(patients);
    }

    /// <summary>
    /// Get repeat appointments count for specified month
    /// </summary>
    [HttpGet("appointments/repeat/{year}/{month}")]
    public IActionResult GetRepeatAppointmentsCount(int year, int month)
    {
        var count = _analyticsService.GetRepeatAppointmentsCountForMonth(year, month);
        return Ok(new CountResponseDto { Count = count });
    }

    /// <summary>
    /// Get patients over specified age with multiple doctors
    /// </summary>
    [HttpGet("patients/age/{age}/multiple-doctors")]
    public IActionResult GetPatientsOverAgeWithMultipleDoctors(int age)
    {
        var patients = _analyticsService.GetPatientsOverAgeWithMultipleDoctors(age);
        return Ok(patients);
    }

    /// <summary>
    /// Get appointments in room for specified month
    /// </summary>
    [HttpGet("appointments/room/{roomNumber}/{year}/{month}")]
    public IActionResult GetAppointmentsInRoomForMonth(int roomNumber, int year, int month)
    {
        var appointments = _analyticsService.GetAppointmentsInRoomForMonth(roomNumber, year, month);
        return Ok(appointments);
    }
}