using Confluent.Kafka;
using System.Text.Json;

/// <summary>
/// Entry point for Kafka contracts generator
/// </summary>
public static class Program
{
    public static async Task Main()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            Acks = Acks.All,
            MessageSendMaxRetries = 5,
            RetryBackoffMs = 1000
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        while (true)
        {
            await producer.ProduceAsync(
                "patients",
                new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(
                        ContractGenerator.GeneratePatient())
                });

            await producer.ProduceAsync(
                "doctors",
                new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(
                        ContractGenerator.GenerateDoctor())
                });

            await producer.ProduceAsync(
                "appointments",
                new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(
                        ContractGenerator.GenerateAppointment())
                });

            await Task.Delay(1000);
        }
    }
}