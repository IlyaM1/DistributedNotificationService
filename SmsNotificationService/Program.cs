using MassTransit;
using SmsNotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(1);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SmsNotificationConsumer, SmsNotificationConsumerDefinition>();

    x.UsingRabbitMq((context, config) =>
    {
        config.ClearSerialization();
        config.UseRawJsonSerializer(RawSerializerOptions.AnyMessageType, isDefault: true);
        config.UseRawJsonDeserializer(RawSerializerOptions.AnyMessageType, isDefault: true);

        config.Host("localhost", "/");
        config.ConfigureEndpoints(context);
    });
});

await builder.Build().RunAsync();
