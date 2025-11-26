using Polyclinic.Contracts.Dto;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Service interface for Patient business operations
/// </summary>
public interface IPatientService
{
    /// <summary>
    /// Get all patients
    /// </summary>
    public List<PatientDto> GetAllPatients();

    /// <summary>
    /// Get patient by identifier
    /// </summary>
    public PatientDto? GetPatientById(int id);

    /// <summary>
    /// Create new patient
    /// </summary>
    public PatientDto CreatePatient(CreatePatientRequest createRequest);

    /// <summary>
    /// Update existing patient
    /// </summary>
    public PatientDto? UpdatePatient(int id, UpdatePatientRequest updateRequest);

    /// <summary>
    /// Delete patient by identifier
    /// </summary>
    public bool DeletePatient(int id);

    /// <summary>
    /// Get patients over specified age with multiple doctors
    /// </summary>
    public List<PatientDto> GetPatientsOverAgeWithMultipleDoctors(int age);
}