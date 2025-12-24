using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnalyticsController(
    IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) 
    : ControllerBase
{

    [HttpGet("doctors/experience/{years:int}")]
    [ProducesResponseType(typeof(List<DoctorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<DoctorDto>> GetDoctorsWithExperienceMoreThan(int years)
    {
        try
        {
            if (years < 0)
            {
                return BadRequest("Years of experience cannot be negative");
            }

            var doctors = analyticsService.GetDoctorsWithExperienceMoreThan(years);
            return Ok(doctors);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting doctors with experience more than {Years} years", years);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("doctors/{id:int}/patients")]
    [ProducesResponseType(typeof(List<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<PatientDto>> GetPatientsByDoctor(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            var patients = analyticsService.GetPatientsByDoctor(id);
            
            if (patients == null || patients.Count == 0)
            {
                return NotFound($"No patients found for doctor with ID {id}");
            }

            return Ok(patients);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting patients for doctor with ID {DoctorId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("appointments/repeat/{year:int}/{month:int}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)] // Изменено на int
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<int> GetRepeatAppointmentsByMonth(int year, int month)
    {
        try
        {
            if (year < 2000 || year > 2100)
            {
                return BadRequest("Year must be between 2000 and 2100");
            }

            if (month < 1 || month > 12)
            {
                return BadRequest("Month must be between 1 and 12");
            }

            var count = analyticsService.GetRepeatAppointmentsCountForMonth(year, month);
            return Ok(count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting repeat appointments count for {Year}/{Month}", year, month);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("patients/age/{age:int}/multiple-doctors")]
    [ProducesResponseType(typeof(List<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<PatientDto>> GetPatientsOlderThanWithMultipleDoctors(int age)
    {
        try
        {
            if (age < 0 || age > 150)
            {
                return BadRequest("Age must be between 0 and 150");
            }

            var patients = analyticsService.GetPatientsOverAgeWithMultipleDoctors(age);
            return Ok(patients);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting patients over age {Age} with multiple doctors", age);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("appointments/room/{room:int}/{year:int}/{month:int}")]
    [ProducesResponseType(typeof(List<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<AppointmentDto>> GetAppointmentsByRoomAndMonth(int room, int year, int month)
    {
        try
        {
            if (room <= 0)
            {
                return BadRequest("Room number must be positive");
            }

            if (year < 2000 || year > 2100)
            {
                return BadRequest("Year must be between 2000 and 2100");
            }

            if (month < 1 || month > 12)
            {
                return BadRequest("Month must be between 1 and 12");
            }

            var appointments = analyticsService.GetAppointmentsInRoomForMonth(room, year, month);
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting appointments for room {Room} in {Year}/{Month}", room, year, month);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    // Остальные методы (count) нужно добавить в интерфейс IAnalyticsService или удалить
}