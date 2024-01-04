using BuildingBlock.Core.Application.PipeBehaviors;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class CqrsExtension
{
    public static IServiceCollection AddCqrs<TApplicationAssemblyReference, TDomainAssemblyReference>(
        this IServiceCollection services)
        where TApplicationAssemblyReference : class
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(TApplicationAssemblyReference));
            cfg.RegisterServicesFromAssemblyContaining(typeof(TDomainAssemblyReference));
            cfg.AddOpenBehavior(typeof(CommandResponseValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandResponseConvertToNullBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandConvertToNullBehavior<,>));
        });

        return services;
    }
}