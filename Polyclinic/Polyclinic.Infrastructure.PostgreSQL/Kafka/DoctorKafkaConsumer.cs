using Confluent.Kafka;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using System.Text.Json;

namespace Polyclinic.Infrastructure.PostgreSql.Kafka;

/// <summary>
/// Kafka consumer for doctor contracts
/// </summary>
public class DoctorKafkaConsumer(
    IDoctorService doctorService)
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "polyclinic-doctors",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("doctors");

        while (!cancellationToken.IsCancellationRequested)
        {
            var result = consumer.Consume(cancellationToken);
            var contract = JsonSerializer.Deserialize<CreateDoctorRequest>(
                result.Message.Value);

            if (contract != null)
                doctorService.CreateDoctor(contract);
        }

        return Task.CompletedTask;
    }
}