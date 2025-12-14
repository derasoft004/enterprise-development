using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DoctorsController(
    IDoctorService doctorService,
    ILogger<DoctorsController> logger) 
    : ControllerBase
{

    /// <summary>
    /// Get all doctors
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<DoctorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<DoctorDto>> GetAllDoctors()
    {
        try
        {
            var doctors = doctorService.GetAllDoctors();
            return Ok(doctors);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all doctors");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Get doctor by ID
    /// </summary>
    /// <param name="id">Doctor ID</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<DoctorDto> GetDoctorById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            var doctor = doctorService.GetDoctorById(id);
            
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {id} not found");
            }

            return Ok(doctor);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting doctor with ID {DoctorId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create new doctor
    /// </summary>
    /// <param name="request">Doctor data</param>
    [HttpPost]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<DoctorDto> CreateDoctor([FromBody] CreateDoctorRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var createdDoctor = doctorService.CreateDoctor(request);
            
            return CreatedAtAction(
                nameof(GetDoctorById),
                new { id = createdDoctor.Id },
                createdDoctor);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error creating doctor");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating doctor");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update doctor
    /// </summary>
    /// <param name="id">Doctor ID</param>
    /// <param name="request">Updated doctor data</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<DoctorDto> UpdateDoctor(
        int id,
        [FromBody] UpdateDoctorRequest request)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var updatedDoctor = doctorService.UpdateDoctor(id, request);
            
            if (updatedDoctor == null)
            {
                return NotFound($"Doctor with ID {id} not found");
            }

            return Ok(updatedDoctor);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error updating doctor with ID {DoctorId}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating doctor with ID {DoctorId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete doctor
    /// </summary>
    /// <param name="id">Doctor ID</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteDoctor(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            var result = doctorService.DeleteDoctor(id);
            
            if (!result)
            {
                return NotFound($"Doctor with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting doctor with ID {DoctorId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}