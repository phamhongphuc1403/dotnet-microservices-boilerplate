using BuildingBlock.Core.Domain.Specifications.Abstractions;

namespace BuildingBlock.Core.Domain.Repositories;

public interface IReadOnlyRepository<TEntity> where TEntity : IEntity
{
    Task<List<TDto>> GetAllAsync<TDto>(ISpecification<TEntity>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false);

    Task<bool> CheckIfExistAsync(ISpecification<TEntity>? specification = null, bool ignoreQueryFilters = false);

    Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<TEntity>? specification,
        string sort, int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false);

    Task<TDto?> GetAnyAsync<TDto>(ISpecification<TEntity>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false);

    Task<TEntity?> GetAnyAsync(ISpecification<TEntity>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false);

    Task<List<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false);

    Task<(List<TEntity>, int)> GetFilterAndPagingAsync(ISpecification<TEntity>? specification,
        string sort, int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false);

    Task<List<TDto>> ToListAsync<TDto>();

    IReadOnlyRepository<TEntity> InitQueryBuilder();

    IReadOnlyRepository<TEntity> AsNoTracking();

    IReadOnlyRepository<TEntity> IgnoreQueryFilters();

    IReadOnlyRepository<TEntity> Join(string includeTables);

    IReadOnlyRepository<TEntity> Where(ISpecification<TEntity> specification);

    IReadOnlyRepository<TEntity> OrderBy(string sort);
}