using Common.Extensions;
using Common.Middlewares;
using TinyCRM.ProductManagement.Application;
using TinyCRM.ProductManagement.EntityFrameworkCore;
using TinyCRM.Service.Product.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddDatabase<ProductDbContext>()
    .AddMapper<Mapper>()
    .AddCqrs<ProductApplicationAssemblyReference>()
    .AddDefaultOpenApi(builder.Configuration)
    .AddDependencyInjection();

var app = builder.Build();

app.UseHttpExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
