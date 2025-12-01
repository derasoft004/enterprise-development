using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.InMemory.Repositories;
using Xunit;

namespace Polyclinic.Tests.Infrastructure;

/// <summary>
/// Tests for InMemory Repositories
/// </summary>
public class RepositoryTests
{
    [Fact]
    public void InMemoryPatientRepository_Create_AddsPatient()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();
        var patient = new Patient 
        { 
            PassportNumber = "1234 567890",
            FullName = "Тестовый Пациент",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        // Act
        var id = repository.Create(patient);

        // Assert
        Assert.True(id > 0);
        var retrieved = repository.Read(id);
        Assert.NotNull(retrieved);
        Assert.Equal("Тестовый Пациент", retrieved.FullName);
    }

    [Fact]
    public void InMemoryPatientRepository_ReadAll_ReturnsAllPatients()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();

        // Act
        var patients = repository.ReadAll();

        // Assert
        Assert.NotNull(patients);
        Assert.NotEmpty(patients); // Должны быть данные из DataSeed
    }
}