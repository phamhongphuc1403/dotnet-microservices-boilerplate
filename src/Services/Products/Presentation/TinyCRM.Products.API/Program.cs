using BuildingBlock.API.Extensions;
using BuildingBlock.API.Middlewares;
using TinyCRM.Products.API.Extensions;
using TinyCRM.Products.Application;
using TinyCRM.Products.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<ProductDbContext, ProductApplicationAssemblyReference>(
    builder.Configuration);

builder.Services.AddProductExtensions(builder.Configuration);

var app = builder.Build();

// await app.UseDefaultMiddlewares(app.Environment);

app.UseHttpExceptionHandler(app.Environment);

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

await app.SeedDataAsync();

app.MapControllers();

app.Run();