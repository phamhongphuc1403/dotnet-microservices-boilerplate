using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Application.IntegrationEvents.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Middlewares;

public static class EventBusMiddleware
{
    public static IApplicationBuilder RegisterEventBusSubcriptions<TApplicationAssemblyReference>(
        this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        var applicationAssembly = typeof(TApplicationAssemblyReference).Assembly;

        var integrationEvents = applicationAssembly.GetTypes().Where(type =>
            type.IsClass && type.IsSubclassOf(typeof(IntegrationEvent)));

        foreach (var integrationEvent in integrationEvents)
        {
            var integrationEventHandlers = applicationAssembly.GetTypes().Where(type =>
                type.IsClass && type.Name == $"{integrationEvent.Name}Handler");

            foreach (var integrationEventHandler in integrationEventHandlers)
                eventBus.Subscribe(integrationEvent, integrationEventHandler);
        }

        return app;
    }
}