using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(
    IAppointmentService appointmentService
    ) : ControllerBase
{

    /// <summary>
    /// Get all appointments
    /// </summary>
    [HttpGet]
    public IActionResult GetAllAppointments()
    {
        var appointments = appointmentService.GetAllAppointments();
        return Ok(appointments);
    }

    /// <summary>
    /// Get appointment by ID
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult GetAppointmentById(int id)
    {
        var appointment = appointmentService.GetAppointmentById(id);
        if (appointment == null)
            return NotFound();
        
        return Ok(appointment);
    }

    /// <summary>
    /// Create new appointment
    /// </summary>
    [HttpPost]
    public IActionResult CreateAppointment([FromBody] CreateAppointmentRequest createRequest)
    {
        try
        {
            var appointment = appointmentService.CreateAppointment(createRequest);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update appointment
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult UpdateAppointment(int id, [FromBody] UpdateAppointmentRequest updateRequest)
    {
        var appointment = appointmentService.UpdateAppointment(id, updateRequest);
        if (appointment == null)
            return NotFound();
        
        return Ok(appointment);
    }

    /// <summary>
    /// Delete appointment
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteAppointment(int id)
    {
        var result = appointmentService.DeleteAppointment(id);
        if (!result)
            return NotFound();
        
        return NoContent();
    }

    /// <summary>
    /// Get appointments by doctor ID
    /// </summary>
    [HttpGet("doctor/{doctorId}")]
    public IActionResult GetAppointmentsByDoctor(int doctorId)
    {
        var appointments = appointmentService.GetAppointmentsByDoctor(doctorId);
        return Ok(appointments);
    }
}