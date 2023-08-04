using TinyCRM.Application.Common.DTOs;
using TinyCRM.Application.Modules.Product.DTOs;

namespace TinyCRM.Application.Modules.Product.Services.Interfaces
{
    public interface IProductService
    {
        Task<GetProductDTO> AddAsync(AddOrUpdateProductDTO dto);

        Task DeleteAsync(Guid id);

        Task<PaginationResponseDTO<GetProductDTO>> GetAllAsync(ProductQueryDTO query);

        Task<GetProductDTO> GetByIdAsync(Guid id);

        Task<GetProductDTO> UpdateAsync(AddOrUpdateProductDTO dto, Guid id);
    }
}