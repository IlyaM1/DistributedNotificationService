using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationGateway.Consumers;
using NotificationGateway.Database;
using NotificationGateway.Database.Repositories;
using NotificationGateway.Handlers;
using NotificationGateway.Interfaces;
using NotificationGateway.Services;
using Scalar.AspNetCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SmsResultConsumer, SmsResultConsumerDefinition>();

    x.UsingRabbitMq((context, config) =>
    {
        config.ClearSerialization();
        config.UseRawJsonSerializer(RawSerializerOptions.AnyMessageType, isDefault: true);
        config.UseRawJsonDeserializer(RawSerializerOptions.AnyMessageType, isDefault: true);

        config.Host("localhost", "/");
        config.ConfigureEndpoints(context);
    });
});

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString).UseLazyLoadingProxies());

builder.Services.AddTransient<INotificationHandler, SmsNotificationHandler>();
builder.Services.AddTransient<SmsNotificationHandler>();
builder.Services.AddTransient<INotificationHandlersFactory, NotificationHandlersFactory>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
builder.Services.AddTransient<INotificationPublisher, NotificationPublisher>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

await app.RunAsync();