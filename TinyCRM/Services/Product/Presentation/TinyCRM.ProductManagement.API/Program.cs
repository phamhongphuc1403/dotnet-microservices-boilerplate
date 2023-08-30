using TinyCRM.ProductManagement.EntityFrameworkCore.Extensions;
using TinyCRM.Service.Product.API.Extensions;
using TinyCRM.Service.Product.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddDatabase()
    .AddMapper()
    .AddCqrs()
    .AddSwagger()
    .AddDependencyInjection();

await builder.Services.ApplyMigrationAsync();

var app = builder.Build();

app.UseHttpExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
