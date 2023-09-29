using BuildingBlock.Common.Extensions;
using BuildingBlock.Common.Middlewares;
using Identities.API.Extensions;
using TinyCRM.Identities.Application;
using TinyCRM.Identities.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<IdentityAppDbContext, IdentityApplicationAssemblyReference>(
    builder.Configuration);

var jwtSetting = builder.Configuration.BindAndGetConfig<JwtSetting>("Jwt");

builder.Services.AddSingleton(jwtSetting);

builder.Services.AddIdentityExtension();

builder.Services.AddAuthenticationExtension();

var app = builder.Build();

app.UseDefaultMiddleware(app.Environment);

app.MapControllers();

app.Run();