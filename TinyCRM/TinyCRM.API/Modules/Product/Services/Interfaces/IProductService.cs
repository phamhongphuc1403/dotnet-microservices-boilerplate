using TinyCRM.API.Modules.Product.DTOs;
using TinyCRM.API.Utilities.PaginationHelper;

namespace TinyCRM.API.Modules.Product.Services.Interfaces
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