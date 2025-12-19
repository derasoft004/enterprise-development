using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Application.Services;


/// <summary>
/// Service implementation for Patient business operations
/// </summary>
public class PatientService( 
    IRepository<Patient, int> patientRepository,
    IRepository<Appointment, int> appointmentRepository
    ) : IPatientService
{

    public List<PatientDto> GetAllPatients()
    {
        var patients = patientRepository.ReadAll();
        return [.. patients.Select(MapToDto)];
    }

    public PatientDto? GetPatientById(int id)
    {
        var patient = patientRepository.Read(id);
        return patient == null ? null : MapToDto(patient);
    }

    public PatientDto CreatePatient(CreatePatientRequest createRequest)
    {
        var patient = new Patient
        {
            PassportNumber = createRequest.PassportNumber,
            FullName = createRequest.FullName,
            DateOfBirth = createRequest.DateOfBirth,
            Gender = createRequest.Gender,
            Address = createRequest.Address,
            BloodGroup = createRequest.BloodGroup,
            ResusFactor = createRequest.ResusFactor,
            PhoneNumber = createRequest.PhoneNumber
        };

        patientRepository.Create(patient);
        return MapToDto(patient);
    }

    public PatientDto? UpdatePatient(int id, UpdatePatientRequest updateRequest)
    {
        var existing = patientRepository.Read(id);
        if (existing == null) return null;

        existing.FullName = updateRequest.FullName;
        existing.Address = updateRequest.Address;
        existing.PhoneNumber = updateRequest.PhoneNumber;

        var updated = patientRepository.Update(id, existing);
        return updated == null ? null : MapToDto(updated);
    }

    public bool DeletePatient(int id)
    {
        return patientRepository.Delete(id);
    }

    public List<PatientDto> GetPatientsOverAgeWithMultipleDoctors(int age)
    {
        var cutoffDate = DateTime.Now.AddYears(-age);
        var appointments = appointmentRepository.ReadAll();

        var patients = appointments
            .GroupBy(a => a.Patient)
            .Where(g => g.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(g => g.Key)
            .Where(p => p.DateOfBirth <= cutoffDate)
            .Select(MapToDto)
            .ToList();

        return patients;
    }

    private static PatientDto MapToDto(Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            FullName = patient.FullName,
            Age = DateTime.Now.Year - patient.DateOfBirth.Year,
            PhoneNumber = patient.PhoneNumber,
            Address = patient.Address,
            Gender = patient.Gender
        };
    }
}