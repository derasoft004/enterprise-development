using System.Net;
using System.Text;
using System.Text.Json;
using Polyclinic.Contracts.Dto;
using Xunit;

namespace Polyclinic.Tests.API;

/// <summary>
/// Integration tests for Polyclinic API
/// </summary>
public class IntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public IntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetPatients_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/api/patients");

        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType?.ToString());

        var content = await response.Content.ReadAsStringAsync();
        var patients = JsonSerializer.Deserialize<List<PatientDto>>(content, _jsonOptions);

        Assert.NotNull(patients);
    }
}
