using Polyclinic.Contracts.Dto;

namespace Polyclinic.Application.Interfaces;

/// <summary>
/// Service interface for Doctor business operations
/// </summary>
public interface IDoctorService
{
    /// <summary>
    /// Get all doctors
    /// </summary>
    public List<DoctorDto> GetAllDoctors();

    /// <summary>
    /// Get doctor by identifier
    /// </summary>
    public DoctorDto? GetDoctorById(int id);

    /// <summary>
    /// Create new doctor
    /// </summary>
    public DoctorDto CreateDoctor(CreateDoctorRequest createRequest);

    /// <summary>
    /// Update existing doctor
    /// </summary>
    public DoctorDto? UpdateDoctor(int id, UpdateDoctorRequest updateRequest);

    /// <summary>
    /// Delete doctor by identifier
    /// </summary>
    public bool DeleteDoctor(int id);

    /// <summary>
    /// Get doctors with experience more than specified years
    /// </summary>
    public List<DoctorDto> GetDoctorsWithExperienceMoreThan(int years);
}