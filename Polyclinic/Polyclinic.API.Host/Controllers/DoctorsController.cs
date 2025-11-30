using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    /// <summary>
    /// Get all doctors
    /// </summary>
    [HttpGet]
    public IActionResult GetAllDoctors()
    {
        var doctors = _doctorService.GetAllDoctors();
        return Ok(doctors);
    }

    /// <summary>
    /// Get doctor by ID
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult GetDoctorById(int id)
    {
        var doctor = _doctorService.GetDoctorById(id);
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
            var doctor = _doctorService.CreateDoctor(createRequest);
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
        var doctor = _doctorService.UpdateDoctor(id, updateRequest);
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
        var result = _doctorService.DeleteDoctor(id);
        if (!result)
            return NotFound();
        
        return NoContent();
    }
}