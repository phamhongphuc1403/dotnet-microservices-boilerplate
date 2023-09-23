using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.Extensions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper<TMapper>(this IServiceCollection services) where TMapper : class
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TMapper)));

        return services;
    }
}