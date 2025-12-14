using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(
    IDoctorService doctorService
    ) : ControllerBase
{


    /// <summary>
    /// Get all doctors
    /// </summary>
    [HttpGet]
    public IActionResult GetAllDoctors()
    {
        var doctors = doctorService.GetAllDoctors();
        return Ok(doctors);
    }

    /// <summary>
    /// Get doctor by ID
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult GetDoctorById(int id)
    {
        var doctor = doctorService.GetDoctorById(id);
        if (doctor == null)
            return NotFound();
        
        return Ok(doctor);
    }

    /// <summary>
    /// Create new doctor
    /// </summary>
    [HttpPost]
    public IActionResult CreateDoctor([FromBody] CreateDoctorRequest createRequest)
    {
        try
        {
            var doctor = doctorService.CreateDoctor(createRequest);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update doctor
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult UpdateDoctor(int id, [FromBody] UpdateDoctorRequest updateRequest)
    {
        var doctor = doctorService.UpdateDoctor(id, updateRequest);
        if (doctor == null)
            return NotFound();
        
        return Ok(doctor);
    }

    /// <summary>
    /// Delete doctor
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteDoctor(int id)
    {
        var result = doctorService.DeleteDoctor(id);
        if (!result)
            return NotFound();
        
        return NoContent();
    }
}