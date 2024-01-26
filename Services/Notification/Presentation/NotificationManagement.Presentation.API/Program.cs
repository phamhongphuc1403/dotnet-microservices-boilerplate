using BuildingBlock.Presentation.API.Extensions;
using BuildingBlock.Presentation.API.Hosts;
using BuildingBlock.Presentation.API.Middlewares;
using NotificationManagement.Core.Application.Notifications;
using NotificationManagement.Core.Domain;
using NotificationManagement.Infrastructure.EntityFrameworkCore;
using NotificationManagement.Presentation.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services
    .AddDefaultExtensions<NotificationApplicationAssemblyReference, NotificationDomainAssemblyReference,
        NotificationDbContext>(builder.Configuration);

builder.Services.AddNotificationExtensions(builder.Configuration);

builder.Host.UseDefaultHosts(builder.Configuration);

var app = builder.Build();

await app.UseDefaultMiddlewares<NotificationApplicationAssemblyReference>(app.Environment, builder.Configuration);

app.MapControllers();

app.Run();