using Polyclinic.Contracts.Dto;

namespace Polyclinic.Kafka;

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
}