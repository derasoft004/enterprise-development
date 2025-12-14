using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticsService analyticsService
    ) : ControllerBase
{


    /// <summary>
    /// Get doctors with experience more than specified years
    /// </summary>
    [HttpGet("doctors/experience/{years}")]
    public IActionResult GetDoctorsWithExperienceMoreThan(int years)
    {
        var doctors = analyticsService.GetDoctorsWithExperienceMoreThan(years);
        return Ok(doctors);
    }

    /// <summary>
    /// Get patients by doctor ID
    /// </summary>
    [HttpGet("doctors/{doctorId}/patients")]
    public IActionResult GetPatientsByDoctor(int doctorId)
    {
        var patients = analyticsService.GetPatientsByDoctor(doctorId);
        return Ok(patients);
    }

    /// <summary>
    /// Get repeat appointments count for specified month
    /// </summary>
    [HttpGet("appointments/repeat/{year}/{month}")]
    public IActionResult GetRepeatAppointmentsCount(int year, int month)
    {
        var count = analyticsService.GetRepeatAppointmentsCountForMonth(year, month);
        return Ok(new CountResponseDto { Count = count });
    }

    /// <summary>
    /// Get patients over specified age with multiple doctors
    /// </summary>
    [HttpGet("patients/age/{age}/multiple-doctors")]
    public IActionResult GetPatientsOverAgeWithMultipleDoctors(int age)
    {
        var patients = analyticsService.GetPatientsOverAgeWithMultipleDoctors(age);
        return Ok(patients);
    }

    /// <summary>
    /// Get appointments in room for specified month
    /// </summary>
    [HttpGet("appointments/room/{roomNumber}/{year}/{month}")]
    public IActionResult GetAppointmentsInRoomForMonth(int roomNumber, int year, int month)
    {
        var appointments = analyticsService.GetAppointmentsInRoomForMonth(roomNumber, year, month);
        return Ok(appointments);
    }
}