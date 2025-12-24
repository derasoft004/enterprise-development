using Polyclinic.Infrastructure.PostgreSql.Kafka;

/// <summary>
/// Background service for appointment Kafka consumer
/// </summary>
public class AppointmentKafkaBackgroundService(
    AppointmentKafkaConsumer consumer)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => consumer.StartAsync(stoppingToken);
}