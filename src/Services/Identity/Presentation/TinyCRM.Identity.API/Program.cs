using BuildingBlock.API.Extensions;
using BuildingBlock.API.Middlewares;
using Identities.API.Extensions;
using Identities.API.GRPC.Services;
using TinyCRM.Identity.Application;
using TinyCRM.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions<IdentityAppDbContext, IdentityApplicationAssemblyReference>(
    builder.Configuration);

var jwtSetting = builder.Configuration.BindAndGetConfig<JwtSetting>("Jwt");

builder.Services.AddSingleton(jwtSetting);

builder.Services
    .AddIdentityExtension()
    .RegisterIdentitySeeder()
    .RegisterIdentityDbContext()
    .RegisterIdentityServices()
    ;

builder.Services.AddGrpc();

builder.Services.AddAuthenticationExtension();

var app = builder.Build();

app.UseDefaultMiddlewares(app.Environment);

await app.SeedDataAsync();

app.MapControllers();

app.MapGrpcService<GrpcAuthService>();

app.Run();