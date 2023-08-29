using System.Reflection;
using TinyCRM.ProductManagement.Application;

namespace TinyCRM.Service.Product.API.Extensions;

public static class Mapper
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TinyCRM.ProductManagement.EntityFrameworkCore.Mapper)));
        
        return services;
    }
}