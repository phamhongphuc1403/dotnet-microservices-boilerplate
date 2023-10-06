using BuildingBlock.API.Extensions;
using BuildingBlock.API.Middlewares;
using BuildingBlock.Application;
using TinyCRM.Products.Application;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<ProductDbContext, ProductApplicationAssemblyReference>(
    builder.Configuration);

builder.Services.AddGrpcAuthentication(builder.Configuration);

builder.Services.RegisterRepositories<Product, ProductDbContext>();

builder.Services.AddScoped<IDataSeeder, ProductSeeder>();

var app = builder.Build();

await app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

app.Run();