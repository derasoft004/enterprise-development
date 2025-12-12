namespace Polyclinic.Contracts.Dto;

/// <summary>
/// Generic analytics response wrapper
/// </summary>
public class AnalyticsResponseDto<T>
{
    /// <summary>
    /// Response data
    /// </summary>
    public T Data { get; set; } = default!;
    
    /// <summary>
    /// Optional message
    /// </summary>
    public string? Message { get; set; }
}