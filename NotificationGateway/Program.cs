using MassTransit;
using Microsoft.AspNetCore.Mvc;
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
    x.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/");
        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddTransient<INotificationPublisher, NotificationPublisher>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

await app.RunAsync();