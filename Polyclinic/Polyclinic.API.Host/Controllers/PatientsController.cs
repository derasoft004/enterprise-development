using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Get all patients
    /// </summary>
    [HttpGet]
    public IActionResult GetAllPatients()
    {
        var patients = _patientService.GetAllPatients();
        return Ok(patients);
    }

    /// <summary>
    /// Get patient by ID
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult GetPatientById(int id)
    {
        var patient = _patientService.GetPatientById(id);
        if (patient == null)
            return NotFound();
        
        return Ok(patient);
    }

    /// <summary>
    /// Create new patient
    /// </summary>
    [HttpPost]
    public IActionResult CreatePatient([FromBody] CreatePatientRequest createRequest)
    {
        try
        {
            var patient = _patientService.CreatePatient(createRequest);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update patient
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult UpdatePatient(int id, [FromBody] UpdatePatientRequest updateRequest)
    {
        var patient = _patientService.UpdatePatient(id, updateRequest);
        if (patient == null)
            return NotFound();
        
        return Ok(patient);
    }

    /// <summary>
    /// Delete patient
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeletePatient(int id)
    {
        var result = _patientService.DeletePatient(id);
        if (!result)
            return NotFound();
        
        return NoContent();
    }
}