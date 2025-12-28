using MassTransit;
using SmsNotificationService.Abstractions;
using SmsNotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqUrl = builder.Configuration["RabbitMqUrl"];

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SmsNotificationConsumer, SmsNotificationConsumerDefinition>();

    x.UsingRabbitMq((context, config) =>
    {
        config.ClearSerialization();
        config.UseRawJsonSerializer(RawSerializerOptions.AnyMessageType, isDefault: true);
        config.UseRawJsonDeserializer(RawSerializerOptions.AnyMessageType, isDefault: true);

        if (!string.IsNullOrEmpty(rabbitMqUrl))
        {
            config.Host(new Uri(rabbitMqUrl));
        }
        else
        {
            config.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        }

        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddTransient<ISmsSender, MockSmsSender>();

await builder.Build().RunAsync();
