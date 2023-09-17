using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;

namespace TinyCRM.Products.Domain.Repositories;

public interface IProductReadOnlyRepository : IReadOnlyRepository<Entities.Product>
{
    Task<(List<Entities.Product>, int)> GetPagedProductsAsync(FilterAndPagingQuery<Entities.Product> query);

    Task<bool> CheckIfCodeExist(string code);

    Task<bool> CheckIfCodeExist(string code, Guid productId);
}