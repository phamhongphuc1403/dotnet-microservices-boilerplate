using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper<TMapper>(this IServiceCollection services) where TMapper : Profile
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TMapper)));
        
        return services;
    }
}