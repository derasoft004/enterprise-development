var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .AddDatabase("polyclinic");

var kafka = builder
    .AddKafka("kafka")
    .WithKafkaUI();

builder.AddProject<Projects.Polyclinic_API_Host>("api")
    .WithReference(postgres)
    .WithReference(kafka);

builder.AddProject<Projects.Polyclinic_Kafka>("kafka-generator")
    .WithReference(kafka);

builder.Build().Run();