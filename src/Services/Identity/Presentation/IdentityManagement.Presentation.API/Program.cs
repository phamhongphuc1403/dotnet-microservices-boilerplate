using BuildingBlock.Presentation.API.Extensions;
using BuildingBlock.Presentation.API.Hosts;
using BuildingBlock.Presentation.API.Middlewares;
using IdentityManagement.Core.Application;
using IdentityManagement.Core.Domain;
using IdentityManagement.Infrastructure.EntityFrameworkCore;
using IdentityManagement.Presentation.API.Extensions;
using IdentityManagement.Presentation.API.GRPC.Services;

var builder = WebApplication.CreateBuilder(args);

await builder.Services
    .AddDefaultExtensions<IdentityApplicationAssemblyReference, IdentityDomainAssemblyReference, AppDbContext>(
        builder.Configuration);

builder.Host.UseDefaultHosts(builder.Configuration);

var jwtSetting = builder.Configuration.BindAndGetConfig<JwtSetting>("Jwt");

builder.Services.AddSingleton(jwtSetting);

builder.Services.AddIdentityExtension(jwtSetting);

builder.Services.AddGrpc();

var app = builder.Build();

await app.UseDefaultMiddlewares(app.Environment);

app.MapControllers();

app.MapGrpcService<GrpcAuthService>();

app.Run();