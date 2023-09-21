using System.Text.Json.Serialization;
using BuildingBlock.Common.Extensions;
using BuildingBlock.Common.Middlewares;
using TinyCRM.Products.API.Extensions;
using TinyCRM.Products.Application;
using TinyCRM.Products.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services
    .AddDatabase<ProductDbContext>(builder.Configuration)
    .AddMapper<Mapper>()
    .AddCqrs<ProductApplicationAssemblyReference>()
    .AddDefaultOpenApi(builder.Configuration)
    .AddDependencyInjection()
    .AddEventBus(builder.Configuration);

await builder.Services.ApplyMigrationAsync<ProductDbContext>();

var app = builder.Build();

app.UseHttpExceptionHandler(app.Services.GetRequiredService<IWebHostEnvironment>());

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();