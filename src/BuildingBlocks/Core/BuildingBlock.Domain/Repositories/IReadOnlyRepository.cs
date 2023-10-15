using BuildingBlock.Domain.Specifications.Abstractions;

namespace BuildingBlock.Domain.Repositories;

public interface IReadOnlyRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity?> GetAnyAsync(ISpecification<TEntity>? specification = null, string? includeTables = null);

    Task<List<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null, string? includeTables = null);

    Task<bool> CheckIfExistAsync(ISpecification<TEntity>? specification = null);

    Task<(List<TEntity>, int)> GetFilterAndPagingAsync(ISpecification<TEntity>? specification,
        string sort, int pageIndex, int pageSize, string? includeTables = null);
}