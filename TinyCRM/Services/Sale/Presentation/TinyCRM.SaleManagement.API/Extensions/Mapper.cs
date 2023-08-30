using System.Reflection;

namespace TinyCRM.SaleManagement.API.Extensions;

public static class Mapper
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TinyCRM.SaleManagement.EntityFrameworkCore.Mapper)));

        return services;
    }
}