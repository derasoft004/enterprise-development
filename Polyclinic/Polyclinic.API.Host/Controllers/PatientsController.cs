using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PatientsController(
    IPatientService patientService,
    ILogger<PatientsController> logger) 
    : ControllerBase
{
    
    /// <summary>
    /// Get all patients
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<PatientDto>> GetAllPatients()
    {
        try
        {
            var patients = patientService.GetAllPatients();
            return Ok(patients);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all patients");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Get patient by ID
    /// </summary>
    /// <param name="id">Patient ID</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<PatientDto> GetPatientById(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            var patient = patientService.GetPatientById(id);
            
            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found");
            }

            return Ok(patient);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting patient with ID {PatientId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create new patient
    /// </summary>
    /// <param name="request">Patient data</param>
    [HttpPost]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<PatientDto> CreatePatient([FromBody] CreatePatientRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var createdPatient = patientService.CreatePatient(request);
            
            return CreatedAtAction(
                nameof(GetPatientById),
                new { id = createdPatient.Id },
                createdPatient);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error creating patient");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating patient");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update patient
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <param name="request">Updated patient data</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public ActionResult<PatientDto> UpdatePatient(
        int id,
        [FromBody] UpdatePatientRequest request)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var updatedPatient = patientService.UpdatePatient(id, request);
            
            if (updatedPatient == null)
            {
                return NotFound($"Patient with ID {id} not found");
            }

            return Ok(updatedPatient);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error updating patient with ID {PatientId}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating patient with ID {PatientId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete patient
    /// </summary>
    /// <param name="id">Patient ID</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult DeletePatient(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            var result = patientService.DeletePatient(id);
            
            if (!result)
            {
                return NotFound($"Patient with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting patient with ID {PatientId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}