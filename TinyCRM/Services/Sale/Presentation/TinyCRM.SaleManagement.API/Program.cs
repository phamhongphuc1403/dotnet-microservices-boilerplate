using Common.Extensions;
using Common.Middlewares;
using TinyCRM.SaleManagement.API.Extensions;
using TinyCRM.SaleManagement.Application;
using TinyCRM.SaleManagement.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services 
    .AddDatabase<SaleDbContext>()
    .AddMapper<Mapper>()
    .AddCqrs<SaleApplicationAssemblyReference>()
    .AddDefaultOpenApi(builder.Configuration)
    .AddDependencyInjection();

await builder.Services.ApplyMigrationAsync<SaleDbContext>();

var app = builder.Build();

app.UseHttpExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();