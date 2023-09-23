using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingBlock.Domain;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}

public interface IDeleteEntity
{
    DateTime DeletedDate { get; set; }
    string DeletedBy { get; set; }
}

public interface IDeleteEntity<TKey> : IDeleteEntity, IBaseEntity<TKey>
{
}

public interface IAuditEntity
{
    DateTime CreatedDate { get; set; }
    string CreatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
    string UpdatedBy { get; set; }
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
    public DateTime DeletedDate { get; set; }
    public string DeletedBy { get; set; } = null!;
}

public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>
{
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; } = null!;
}

public abstract class Entity : AuditEntity<Guid>
{
}

public abstract class AggregateRoot : Entity
{
}