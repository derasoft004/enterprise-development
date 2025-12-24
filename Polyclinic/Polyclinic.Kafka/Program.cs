using Confluent.Kafka;
using System.Text.Json;

/// <summary>
/// Entry point for contracts generator service
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
            var contract = ContractGenerator.GeneratePatient();

            var json = JsonSerializer.Serialize(contract);

            await producer.ProduceAsync(
                "patients",
                new Message<Null, string> { Value = json });

            await Task.Delay(1000);
        }
    }
}