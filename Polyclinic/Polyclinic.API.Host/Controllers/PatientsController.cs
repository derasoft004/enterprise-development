using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;

namespace Polyclinic.API.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(
    IPatientService patientService
    ): ControllerBase
{


    /// <summary>
    /// Get all patients
    /// </summary>
    [HttpGet]
    public IActionResult GetAllPatients()
    {
        var patients = patientService.GetAllPatients();
        return Ok(patients);
    }

    /// <summary>
    /// Get patient by ID
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult GetPatientById(int id)
    {
        var patient = patientService.GetPatientById(id);
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
            var patient = patientService.CreatePatient(createRequest);
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
        var patient = patientService.UpdatePatient(id, updateRequest);
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
        var result = patientService.DeletePatient(id);
        if (!result)
            return NotFound();
        
        return NoContent();
    }
}