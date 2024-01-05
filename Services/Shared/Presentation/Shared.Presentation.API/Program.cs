using BuildingBlock.Presentation.API.Hosts;
using Shared.Presentation.API.Extensions;
using Shared.Presentation.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedExtensions(builder.Configuration);

builder.Host.UseDefaultHosts(builder.Configuration);

var app = builder.Build();

await app.UseSharedMiddlewaresAsync(app.Environment, builder.Configuration);

app.MapControllers();

app.Run();