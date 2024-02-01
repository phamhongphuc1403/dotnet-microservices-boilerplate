using BuildingBlock.Presentation.API.Extensions;
using BuildingBlock.Presentation.API.Hosts;
using BuildingBlock.Presentation.API.Middlewares;
using BuildingBlock.Presentation.API.Utilities;
using IdentityManagement.Core.Application;
using IdentityManagement.Core.Application.Shared;
using IdentityManagement.Core.Domain;
using IdentityManagement.Infrastructure.EntityFrameworkCore;
using IdentityManagement.Presentation.API.Extensions;
using IdentityManagement.Presentation.API.GRPC.Services;

var builder = WebApplication.CreateBuilder(args);

await builder.Services
    .AddDefaultExtensions<IdentityApplicationAssemblyReference, IdentityDomainAssemblyReference, AppDbContext>(
        builder.Configuration);

builder.Host.UseDefaultHosts(builder.Configuration);

var jwtConfiguration = builder.Configuration.BindAndGetConfig<JwtConfiguration>("Jwt");

builder.Services.AddSingleton(jwtConfiguration);

builder.Services.AddIdentityExtension(jwtConfiguration);

builder.Services.AddGrpc();

var app = builder.Build();

await app.UseDefaultMiddlewares<IdentityApplicationAssemblyReference>(app.Environment, builder.Configuration);

app.MapControllers();

app.MapGrpcService<GrpcAuthService>();

app.Run();