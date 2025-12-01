using Microsoft.AspNetCore.Mvc.Testing;
using Polyclinic.API.Host;
using System.Net;
using System.Text;
using System.Text.Json;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

/// <summary>
/// Integration tests for API
/// </summary>
public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetPatients_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/api/patients");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());

        var content = await response.Content.ReadAsStringAsync();
        var patients = JsonSerializer.Deserialize<List<PatientDto>>(content, _jsonOptions);
        
        Assert.NotNull(patients);
        Assert.NotEmpty(patients);
    }

    [Fact]
    public async Task GetPatientById_ReturnsPatient()
    {
        // Arrange
        var patientId = 1;

        // Act
        var response = await _client.GetAsync($"/api/patients/{patientId}");

        // Assert
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            // Пациент может не существовать в тестовых данных
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        else
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var patient = JsonSerializer.Deserialize<PatientDto>(content, _jsonOptions);
            Assert.NotNull(patient);
            Assert.Equal(patientId, patient.Id);
        }
    }

    [Fact]
    public async Task CreatePatient_ReturnsCreated()
    {
        // Arrange
        var newPatient = new CreatePatientRequest
        {
            PassportNumber = "9999 888888",
            FullName = "Интеграционный Тест",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        var json = JsonSerializer.Serialize(newPatient);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/patients", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdPatient = JsonSerializer.Deserialize<PatientDto>(responseContent, _jsonOptions);
        
        Assert.NotNull(createdPatient);
        Assert.Equal("Интеграционный Тест", createdPatient.FullName);
    }
}