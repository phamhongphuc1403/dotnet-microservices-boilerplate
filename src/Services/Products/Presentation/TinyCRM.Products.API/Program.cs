using BuildingBlock.API.Extensions;
using BuildingBlock.API.Middlewares;
using TinyCRM.Products.Application;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<ProductDbContext, ProductApplicationAssemblyReference>(
    builder.Configuration);

builder.Services.RegisterRepositories<Product, ProductDbContext>();

var app = builder.Build();

app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

app.Run();