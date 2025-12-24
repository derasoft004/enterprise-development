using Confluent.Kafka;
using Polyclinic.Application.Interfaces;
using Polyclinic.Contracts.Dto;
using System.Text.Json;

namespace Polyclinic.Infrastructure.PostgreSql.Kafka;

/// <summary>
/// Kafka consumer for patient contracts
/// </summary>
public class PatientKafkaConsumer(
    IPatientService patientService)
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "polyclinic-patients",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("patients");

        while (!cancellationToken.IsCancellationRequested)
        {
            var result = consumer.Consume(cancellationToken);

            var contract = JsonSerializer.Deserialize<CreatePatientRequest>(
                result.Message.Value);

            if (contract != null)
                patientService.CreatePatient(contract);
        }

        return Task.CompletedTask;
    }
}