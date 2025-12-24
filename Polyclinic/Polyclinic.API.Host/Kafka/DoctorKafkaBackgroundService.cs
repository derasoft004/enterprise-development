using Polyclinic.Infrastructure.PostgreSql.Kafka;

/// <summary>
/// Background service for doctor Kafka consumer
/// </summary>
public class DoctorKafkaBackgroundService(
    DoctorKafkaConsumer consumer)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => consumer.StartAsync(stoppingToken);
}