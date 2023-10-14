using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingBlock.Domain;

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
    public string? UpdatedBy { get; set; } = null!;
}

public interface IAggregateRoot<TKey> : IEntity<TKey>
{
}

public interface IEntity<TKey> : IAuditEntity<TKey>
{
}

public abstract class Entity : AuditEntity<Guid>, IEntity<Guid>
{
}

public abstract class AggregateRoot : Entity, IAggregateRoot<Guid>
{
}