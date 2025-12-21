namespace Polyclinic.Infrastructure.PostgreSQL.Configurations;

/// <summary>
/// PostgreSQL Connection Settings
/// </summary>
public class PostgresSettings
{
    public const string SectionName = "PostgreSQL";

    /// <summary>
    /// Connection string
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Database Name
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;
}