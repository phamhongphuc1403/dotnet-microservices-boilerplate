using TinyCRM.API.Modules.Product.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Product.Services
{
    public interface IProductService
    {
        Task<GetProductDto> AddAsync(AddOrUpdateProductDto dto);

        Task DeleteAsync(Guid id);

        Task<PaginationResponse<GetProductDto>> GetAllAsync(ProductQueryDTO query);

        Task<GetProductDto> GetByIdAsync(Guid id);

        Task<GetProductDto> UpdateAsync(AddOrUpdateProductDto dto, Guid id);
    }
}