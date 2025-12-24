using Confluent.Kafka;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using System.Text.Json;

namespace Polyclinic.Infrastructure.PostgreSql.Kafka;

/// <summary>
/// Kafka consumer for appointment contracts
/// </summary>
public class AppointmentKafkaConsumer(
    IAppointmentService appointmentService)
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "polyclinic-appointments",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("appointments");

        while (!cancellationToken.IsCancellationRequested)
        {
            var result = consumer.Consume(cancellationToken);
            var contract = JsonSerializer.Deserialize<CreateAppointmentRequest>(
                result.Message.Value);

            if (contract != null)
                appointmentService.CreateAppointment(contract);
        }

        return Task.CompletedTask;
    }
}