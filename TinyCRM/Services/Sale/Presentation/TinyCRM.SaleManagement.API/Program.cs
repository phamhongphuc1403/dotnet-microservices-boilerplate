using BuildingBLock.Common.Extensions;
using BuildingBLock.Common.Middlewares;
using BuildingBlock.Core.EventBus.Interfaces;
using TinyCRM.SaleManagement.API.Extensions;
using TinyCRM.SaleManagement.Application;
using TinyCRM.SaleManagement.Application.IntegrationEvents.Events;
using TinyCRM.SaleManagement.Application.IntegrationEvents.Handlers;
using TinyCRM.SaleManagement.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services 
    .AddDatabase<SaleDbContext>()
    .AddMapper<Mapper>()
    .AddCqrs<SaleApplicationAssemblyReference>()
    .AddDefaultOpenApi(builder.Configuration)
    .AddDependencyInjection()
    .AddEventBus(builder.Configuration);

builder.Services.AddTransient<ProductCreatedIntegrationEventHandler>();

await builder.Services.ApplyMigrationAsync<SaleDbContext>();

var app = builder.Build();

app.UseHttpExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();

app.Run();