using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Common.Extensions;
using BuildingBlock.Common.Middlewares;
using TinyCRM.Sales.API.Extensions;
using TinyCRM.Sales.Application;
using TinyCRM.Sales.Application.IntegrationEvents.Events;
using TinyCRM.Sales.Application.IntegrationEvents.Handlers;
using TinyCRM.Sales.EntityFrameworkCore;

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