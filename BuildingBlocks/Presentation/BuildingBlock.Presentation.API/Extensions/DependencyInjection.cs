using System.Text.RegularExpressions;
using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static partial class DependencyInjection
{
    public static IServiceCollection RegisterServices<TAssemblyReference>(this IServiceCollection services)
    {
        var assembly = typeof(TAssemblyReference).Assembly;

        var classesToRegister = assembly.GetTypes()
            .Where(type => type.IsClass && Service().IsMatch(type.Name));

        foreach (var classType in classesToRegister)
        {
            var interfaceType = classType.GetInterfaces()
                .FirstOrDefault(type => Service().IsMatch(type.Name));

            if (interfaceType != null) services.AddScoped(interfaceType, classType);
        }

        return services;
    }

    [GeneratedRegex("^.+Service$")]
    private static partial Regex Service();

    public static IServiceCollection RegisterDefaultRepositories<TDomainAssemblyReference, TDbContext>(
        this IServiceCollection services)
        where TDomainAssemblyReference : DomainAssemblyReference
        where TDbContext : DbContext
    {
        var domainAssembly = typeof(TDomainAssemblyReference).Assembly;

        var entityTypes = domainAssembly.GetTypes()
            .Where(type => typeof(IEntity).IsAssignableFrom(type) && type.IsClass);

        var aggregateRootTypes = domainAssembly.GetTypes()
            .Where(type => typeof(IAggregateRoot).IsAssignableFrom(type) && type.IsClass);

        var dbContextAssembly = typeof(TDbContext).Assembly;

        var dbContextSubclasses = Optional<Type>
            .Of(dbContextAssembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(DbContext)) && t.IsClass))
            .ThrowIfNotExist(new InvalidOperationException("Cannot find DbContext subclass")).Get();

        foreach (var entity in entityTypes)
        {
            var iReadOnlyRepository = typeof(IReadOnlyRepository<>).MakeGenericType(entity);
            var readOnlyRepository = typeof(ReadOnlyRepository<,>).MakeGenericType(dbContextSubclasses, entity);

            services.AddScoped(iReadOnlyRepository, readOnlyRepository);
        }

        foreach (var aggregateRooot in aggregateRootTypes)
        {
            var iOperationRepository = typeof(IOperationRepository<>).MakeGenericType(aggregateRooot);
            var operationRepository =
                typeof(OperationRepository<,>).MakeGenericType(dbContextSubclasses, aggregateRooot);

            services.AddScoped(iOperationRepository, operationRepository);
        }

        return services;
    }

    public static IServiceCollection RegisterCustomRepositories<TDbContext>(
        this IServiceCollection services)
        where TDbContext : DbContext
    {
        var dbContextAssembly = typeof(TDbContext).Assembly;

        var customClasses = dbContextAssembly.GetTypes()
            .Where(type => CustomRepository().IsMatch(type.Name) && type.IsClass);

        foreach (var customClass in customClasses)
        {
            var customInterface = customClass.GetInterfaces()
                .FirstOrDefault(type => $"I{customClass.Name}" == type.Name);

            if (customInterface == null) continue;

            services.AddScoped(customInterface, customClass);
        }

        return services;
    }

    [GeneratedRegex("^.+((ReadOnly|Operation)Repository)$")]
    private static partial Regex CustomRepository();

    public static IServiceCollection RegisterCachedRepositories<TDbContext>(
        this IServiceCollection services)
        where TDbContext : DbContext
    {
        var dbContextAssembly = typeof(TDbContext).Assembly;

        var cachedClasses = dbContextAssembly.GetTypes()
            .Where(type => CachedRepository().IsMatch(type.Name) && type.IsClass);

        foreach (var cachedClass in cachedClasses)
        {
            var cachedInterface = cachedClass.GetInterfaces()
                .FirstOrDefault(type => CustomRepository().IsMatch(type.Name));

            if (cachedInterface == null) continue;

            services.Decorate(cachedInterface, cachedClass);
        }

        return services;
    }

    [GeneratedRegex("^Cached.+Repository$")]
    private static partial Regex CachedRepository();

    public static IServiceCollection RegisterSeeders<TApplicationAssemblyReference>(this IServiceCollection services)
        where TApplicationAssemblyReference : ApplicationAssemblyReference
    {
        var applicationAssembly = typeof(TApplicationAssemblyReference).Assembly;

        var seederClasses =
            applicationAssembly.GetTypes().Where(type => Seeder().IsMatch(type.Name) && type.IsClass);

        foreach (var seederClass in seederClasses)
            if (seederClass.GetInterface("IDataSeeder") != null)
                services.AddScoped(typeof(IDataSeeder), seederClass);

        return services;
    }

    [GeneratedRegex("^.+Seeder")]
    private static partial Regex Seeder();

    public static IServiceCollection RegisterUnitOfWork<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        var dbContextAssembly = typeof(TDbContext).Assembly;

        var unitOfWorkClass = dbContextAssembly.GetTypes()
            .FirstOrDefault(type => type.Name.Contains("UnitOfWork") && type.IsClass);

        if (unitOfWorkClass != null && unitOfWorkClass.GetInterface("IUnitOfWork") != null)
            services.AddScoped(typeof(IUnitOfWork), unitOfWorkClass);

        return services;
    }

    public static IServiceCollection RegisterIntegrationEventHandlers<TApplicationAssemblyReference>(
        this IServiceCollection services)
    {
        var applicationAssembly = typeof(TApplicationAssemblyReference).Assembly;

        var integrationEventClasses = applicationAssembly.GetTypes()
            .Where(type => type.IsClass && type.Name.Contains("IntegrationEventHandler"));

        foreach (var integrationEventClass in integrationEventClasses) services.AddTransient(integrationEventClass);

        return services;
    }
}