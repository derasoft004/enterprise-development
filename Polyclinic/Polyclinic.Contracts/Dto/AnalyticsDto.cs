// Polyclinic.Contracts/Dto/AnalyticsDto.cs
namespace Polyclinic.Contracts.Dto;

public class CountResponseDto
{
    public int Count { get; set; }
}

// Или более общий
public class AnalyticsResponseDto<T>
{
    public T Data { get; set; } = default!;
    public string? Message { get; set; }
}