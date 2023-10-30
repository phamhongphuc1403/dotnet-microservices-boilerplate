using BuildingBlock.Application.PipeBehaviors;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.API.Extensions;

public static class CqrsExtension
{
    public static IServiceCollection AddCqrs<TApplicationAssemblyReference>(this IServiceCollection services)
        where TApplicationAssemblyReference : class
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(TApplicationAssemblyReference));
            cfg.AddOpenBehavior(typeof(CommandResponseValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandResponseConvertToNullBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandConvertToNullBehavior<,>));
        });

        return services;
    }
}