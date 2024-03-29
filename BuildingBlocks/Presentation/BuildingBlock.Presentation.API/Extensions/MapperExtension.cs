using System.Reflection;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper<TMapper>(this IServiceCollection services) where TMapper : class
    {
        services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); },
            Assembly.GetAssembly(typeof(TMapper)));

        return services;
    }
}