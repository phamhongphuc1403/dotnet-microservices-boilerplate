using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Product.DTOs;

namespace TinyCRM.Application.Modules.Product.Services.Interfaces
{
    public interface IProductService
    {
        Task<GetProductDto> AddAsync(AddOrUpdateProductDto dto);

        Task DeleteAsync(Guid id);

        Task<PaginationResponseDto<GetProductDto>> GetAllAsync(ProductQueryDto query);

        Task<GetProductDto> GetByIdAsync(Guid id);

        Task<GetProductDto> UpdateAsync(AddOrUpdateProductDto dto, Guid id);
    }
}