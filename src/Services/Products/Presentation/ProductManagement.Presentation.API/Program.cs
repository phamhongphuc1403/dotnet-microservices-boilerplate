using BuildingBlock.Presentation.API.Extensions;
using BuildingBlock.Presentation.API.Hosts;
using BuildingBlock.Presentation.API.Middlewares;
using ProductManagement.Core.Application;
using ProductManagement.Core.Domain;
using ProductManagement.Infrastructure.EntityFrameworkCore;
using ProductManagement.Presentation.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services
    .AddDefaultExtensions<ProductApplicationAssemblyReference, ProductDomainAssemblyReference, ProductDbContext>(
        builder.Configuration);

builder.Services.AddProductExtensions(builder.Configuration);

builder.Host.UseDefaultHosts(builder.Configuration);

var app = builder.Build();

await app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

app.Run();