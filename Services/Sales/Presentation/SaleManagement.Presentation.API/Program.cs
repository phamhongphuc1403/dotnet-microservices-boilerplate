using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Presentation.API.Extensions;
using BuildingBlock.Presentation.API.Hosts;
using BuildingBlock.Presentation.API.Middlewares;
using SaleManagement.Core.Application;
using SaleManagement.Core.Application.IntegrationEvents.Events;
using SaleManagement.Core.Application.IntegrationEvents.Handlers;
using SaleManagement.Core.Domain;
using SaleManagement.Infrastructure.EntityFrameworkCore;
using SaleManagement.Presentation.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services
    .AddDefaultExtensions<SaleApplicationAssemblyReference, SaleDomainAssemblyReference, SaleDbContext>(
        builder.Configuration);

builder.Services.AddSaleExtensions(builder.Configuration);

builder.Host.UseDefaultHosts(builder.Configuration);

var app = builder.Build();

await app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();
eventBus.Subscribe<ProductEditedIntegrationEvent, ProductEditedIntegrationEventHandler>();
eventBus.Subscribe<ProductDeletedIntegrationEvent, ProductDeletedIntegrationEventHandler>();

app.Run();