using BuildingBlock.Domain.Specifications.Abstractions;

namespace BuildingBlock.Domain.Repositories;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetAnyAsync(ISpecification<TEntity> specification, string? includeTables = null);

    Task<List<TEntity>> GetAllAsync(ISpecification<TEntity> specification, string? includeTables = null);

    Task<bool> CheckIfExistAsync(ISpecification<TEntity> specification);

    Task<(List<TEntity>, int)> GetFilterAndPagingAsync(ISpecification<TEntity> specification,
        string sort, int pageIndex, int pageSize, string? includeTables = null);
}