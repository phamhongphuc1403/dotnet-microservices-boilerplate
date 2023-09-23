using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Common.Middlewares;
using TinyCRM.Sales.API.Extensions;
using TinyCRM.Sales.Application.IntegrationEvents.Events;
using TinyCRM.Sales.Application.IntegrationEvents.Handlers;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions(builder.Configuration);

var app = builder.Build();

var environment = app.Environment;

app.UseDefaultMiddleware(environment);

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();

app.Run();