using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Application.Services;

/// <summary>
/// Service implementation for Doctor business operations
/// </summary>
public class DoctorService(
    IRepository<Doctor, int> doctorRepository,
    IRepository<Specialization, int> specializationRepository
    ) : IDoctorService
{
    public List<DoctorDto> GetAllDoctors()
    {
        var doctors = doctorRepository.ReadAll();
        return doctors.Select(MapToDto).ToList();
    }

    public DoctorDto? GetDoctorById(int id)
    {
        var doctor = doctorRepository.Read(id);
        return doctor == null ? null : MapToDto(doctor);
    }

    public DoctorDto CreateDoctor(CreateDoctorRequest createRequest)
    {
        var specialization = specializationRepository.Read(createRequest.SpecializationId);
        if (specialization == null)
            throw new ArgumentException($"Specialization with ID {createRequest.SpecializationId} not found");

        var doctor = new Doctor
        {
            PassportNumber = createRequest.PassportNumber,
            FullName = createRequest.FullName,
            YearOfBirth = createRequest.YearOfBirth,
            Specialization = specialization,
            Experience = createRequest.Experience
        };

        var id = doctorRepository.Create(doctor);
        return MapToDto(doctor);
    }

    public DoctorDto? UpdateDoctor(int id, UpdateDoctorRequest updateRequest)
    {
        var existing = doctorRepository.Read(id);
        if (existing == null) return null;

        var specialization = specializationRepository.Read(updateRequest.SpecializationId);
        if (specialization == null)
            throw new ArgumentException($"Specialization with ID {updateRequest.SpecializationId} not found");

        existing.FullName = updateRequest.FullName;
        existing.YearOfBirth = updateRequest.YearOfBirth;
        existing.Specialization = specialization;
        existing.Experience = updateRequest.Experience;

        var updated = doctorRepository.Update(id, existing);
        return updated == null ? null : MapToDto(updated);
    }

    public bool DeleteDoctor(int id)
    {
        return doctorRepository.Delete(id);
    }

    public List<DoctorDto> GetDoctorsWithExperienceMoreThan(int years)
    {
        var doctors = doctorRepository.ReadAll()
            .Where(d => d.Experience >= years)
            .Select(MapToDto)
            .ToList();

        return doctors;
    }

    private static DoctorDto MapToDto(Doctor doctor)
    {
        var experienceLevel = doctor.Experience switch
        {
            < 5 => "Junior",
            < 15 => "Middle", 
            _ => "Senior"
        };

        return new DoctorDto
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Specialization = doctor.Specialization.Name,
            Experience = doctor.Experience ?? 0,
            ExperienceLevel = experienceLevel
        };
    }
}