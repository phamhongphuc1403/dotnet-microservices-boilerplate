using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using BuildingBlock.Core.Domain.DomainEvents;

namespace BuildingBlock.Core.Domain;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}

public interface IDeleteEntity
{
    DateTime? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
}

public interface IDeleteEntity<TKey> : IDeleteEntity, IBaseEntity<TKey>
{
}

public interface IAuditEntity
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }
}

public interface IAuditEntity<TKey> : IAuditEntity, IDeleteEntity<TKey>
{
}

public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TKey Id { get; set; } = default!;
}

public abstract class DeleteEntity<TKey> : BaseEntity<TKey>, IDeleteEntity<TKey>
{
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}

public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public interface IAggregateRoot : IEntity
{
}

public interface IEntity : IAuditEntity<Guid>
{
}

public abstract class Entity : AuditEntity<Guid>, IEntity
{
    public void ResetUpdatedTimeStamp()
    {
        UpdatedAt = null;
        UpdatedBy = null;
    }

    public void Delete(DateTime? deletedAt, string? deletedBy)
    {
        DeletedAt ??= deletedAt;
        DeletedBy ??= deletedBy;

        var entityProperties = GetType().GetProperties();

        foreach (var entityProperty in entityProperties)
            if (IsAGenericList(entityProperty.PropertyType))
                DeleteProperty(entityProperty, deletedAt, deletedBy);
    }

    private void DeleteProperty(PropertyInfo propertyInfo, DateTime? deletedAt, string? deletedBy)
    {
        var propertyValues = GetPropertyValues(propertyInfo);

        var deleteMethod = GetDeleteMethod(propertyInfo);

        if (deleteMethod == null) return;

        foreach (var value in propertyValues)
        {
            object?[] parameters = { deletedAt, deletedBy };
            deleteMethod.Invoke(value, parameters);
        }
    }

    private static MethodInfo? GetDeleteMethod(PropertyInfo propertyInfo)
    {
        var elementType = propertyInfo.PropertyType.GetGenericArguments()[0];

        return elementType.GetMethod("Delete");
    }

    private IEnumerable<object?> GetPropertyValues(PropertyInfo propertyInfo)
    {
        var values = propertyInfo.GetValue(this);

        return values is null ? new List<object?>() : (IEnumerable<object?>)values;
    }

    private static bool IsAGenericList(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
    }
}

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    public List<IDomainEvent> DomainEvents { get; } = new();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }
}