using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Common.Extensions;
using BuildingBlock.Common.Middlewares;
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

app.UseDefaultMiddleware(app.Environment);

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();

app.Run();