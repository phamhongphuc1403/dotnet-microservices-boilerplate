using BuildingBlock.Common.Middlewares;
using TinyCRM.Products.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions(builder.Configuration);

var app = builder.Build();

app.UseDefaultMiddleware(app.Environment);

app.MapControllers();

app.Run();