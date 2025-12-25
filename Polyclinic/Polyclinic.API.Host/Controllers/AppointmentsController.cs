using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AppointmentsController(
    IAppointmentService appointmentService,
    ILogger<AppointmentsController> logger) 
    : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<AppointmentDto>> GetAllAppointments()
    {
        try
        {
            var appointments = appointmentService.GetAllAppointments();
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all appointments");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AppointmentDto> GetAppointmentById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }

            var appointment = appointmentService.GetAppointmentById(id);
            
            if (appointment == null)
            {
                return NotFound($"Appointment with ID {id} not found");
            }

            return Ok(appointment);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting appointment with ID {AppointmentId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AppointmentDto> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var createdAppointment = appointmentService.CreateAppointment(request);
            
            return CreatedAtAction(
                nameof(GetAppointmentById),
                new { id = createdAppointment.Id },
                createdAppointment);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error creating appointment");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating appointment");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<AppointmentDto> UpdateAppointment(
        int id,
        [FromBody] UpdateAppointmentRequest request)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var updatedAppointment = appointmentService.UpdateAppointment(id, request);
            
            if (updatedAppointment == null)
            {
                return NotFound($"Appointment with ID {id} not found");
            }

            return Ok(updatedAppointment);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error updating appointment with ID {AppointmentId}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating appointment with ID {AppointmentId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteAppointment(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }

            var result = appointmentService.DeleteAppointment(id);
            
            if (!result)
            {
                return NotFound($"Appointment with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting appointment with ID {AppointmentId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}