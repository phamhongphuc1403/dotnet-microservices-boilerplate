using BuildingBlock.API.Extensions;
using BuildingBlock.API.Middlewares;
using BuildingBlock.Application.EventBus.Abstractions;
using TinyCRM.Sales.Application;
using TinyCRM.Sales.Application.IntegrationEvents.Events;
using TinyCRM.Sales.Application.IntegrationEvents.Handlers;
using TinyCRM.Sales.Domain.DealAggregate.Entities;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;
using TinyCRM.Sales.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<SaleDbContext, SaleApplicationAssemblyReference>(builder.Configuration);

builder.Services.RegisterRepositories<Deal, SaleDbContext>();
builder.Services.RegisterRepositories<Lead, SaleDbContext>();

builder.Services.AddTransient<ProductCreatedIntegrationEventHandler>();

var app = builder.Build();

app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();

app.Run();