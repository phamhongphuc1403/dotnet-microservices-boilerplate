using BuildingBlock.API.Extensions;
using BuildingBlock.API.Middlewares;
using Identities.API.Extensions;
using Identities.API.GRPC.Services;
using TinyCRM.Identity.Application;
using TinyCRM.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<AppDbContext, IdentityApplicationAssemblyReference>(
    builder.Configuration);

var jwtSetting = builder.Configuration.BindAndGetConfig<JwtSetting>("Jwt");

builder.Services.AddSingleton(jwtSetting);

builder.Services.AddIdentityExtension();

builder.Services.AddGrpc();

builder.Services.AddAuthenticationExtension(jwtSetting);

var app = builder.Build();

await app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

app.MapGrpcService<GrpcAuthService>();

app.Run();