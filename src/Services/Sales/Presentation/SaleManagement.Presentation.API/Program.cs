using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using BuildingBlock.Presentation.API.Extensions;
using BuildingBlock.Presentation.API.Hosts;
using BuildingBlock.Presentation.API.Middlewares;
using SaleManagement.Core.Application;
using SaleManagement.Core.Application.IntegrationEvents.Events;
using SaleManagement.Core.Application.IntegrationEvents.Handlers;
using SaleManagement.Core.Domain.DealAggregate.Entities;
using SaleManagement.Core.Domain.LeadAggregate.Entities;
using SaleManagement.Infrastructure.EntityFrameworkCore;

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