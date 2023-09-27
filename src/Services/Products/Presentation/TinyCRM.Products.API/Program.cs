using BuildingBlock.Common.Extensions;
using BuildingBlock.Common.Middlewares;
using TinyCRM.Products.Application;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<ProductDbContext, ProductApplicationAssemblyReference>(
    builder.Configuration);

builder.Services.RegisterRepositories<Product, ProductDbContext>();

var app = builder.Build();

app.UseDefaultMiddleware(app.Environment);

app.MapControllers();

app.Run();