using Polyclinic.Contracts.Dto;

/// <summary>
/// Factory for generating contracts
/// </summary>
public static class ContractGenerator
{
    public static CreatePatientRequest GeneratePatient()
        => new()
        {
            PassportNumber = Guid.NewGuid().ToString(),
            FullName = $"Patient {Random.Shared.Next(1, 1000)}",
            DateOfBirth = DateTime.Now.AddYears(-Random.Shared.Next(18, 80))
        };

    public static CreateDoctorRequest GenerateDoctor()
        => new()
        {
            PassportNumber = Guid.NewGuid().ToString(),
            FullName = $"Doctor {Random.Shared.Next(1, 1000)}",
            SpecializationId = 1,
            Experience = Random.Shared.Next(1, 40)
        };

    public static CreateAppointmentRequest GenerateAppointment()
        => new()
        {
            PatientId = 1,
            DoctorId = 1,
            AppointmentDateTime = DateTime.Now.AddDays(Random.Shared.Next(1, 30)),
            RoomNumber = Random.Shared.Next(100, 500),
        };
}