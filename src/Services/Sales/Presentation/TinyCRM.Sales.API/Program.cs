using BuildingBlock.API.Extensions;
using BuildingBlock.API.Hosts;
using BuildingBlock.API.Middlewares;
using BuildingBlock.Application.EventBus.Abstractions;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Sales.Application;
using TinyCRM.Sales.Application.IntegrationEvents.Events;
using TinyCRM.Sales.Application.IntegrationEvents.Handlers;
using TinyCRM.Sales.Domain.DealAggregate.Entities;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;
using TinyCRM.Sales.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<SaleDbContext, SaleApplicationAssemblyReference>(builder.Configuration);

builder.Services.AddScoped<IOperationRepository<Lead>, OperationRepository<SaleDbContext, Lead>>();
builder.Services.AddScoped<IReadOnlyRepository<Lead>, ReadOnlyRepository<SaleDbContext, Lead>>();

builder.Services.AddScoped<IOperationRepository<Deal>, OperationRepository<SaleDbContext, Deal>>();
builder.Services.AddScoped<IReadOnlyRepository<Deal>, ReadOnlyRepository<SaleDbContext, Deal>>();

builder.Services.AddTransient<ProductCreatedIntegrationEventHandler>();


builder.Host.UseDefaultHosts(builder.Configuration);

var app = builder.Build();

await app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();

app.Run();