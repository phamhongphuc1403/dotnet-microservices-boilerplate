using System.Reflection;
using BuildingBlock.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore.Extensions;

public static class FilterExtension
{
    private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(FilterExtension)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Single(t => t is { IsGenericMethod: true, Name: "SetSoftDeleteFilter" });

    public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
    {
        SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
            .Invoke(null, new object[] { modelBuilder });
    }

    public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : class, IEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(entity => entity.DeletedAt == null);
    }
}