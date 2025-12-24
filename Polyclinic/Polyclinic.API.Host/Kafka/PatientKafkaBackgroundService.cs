using Polyclinic.Infrastructure.PostgreSql.Kafka;

/// <summary>
/// Background service for Kafka patient consumer
/// </summary>
public class PatientKafkaBackgroundService(
    PatientKafkaConsumer consumer)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => consumer.StartAsync(stoppingToken);
}