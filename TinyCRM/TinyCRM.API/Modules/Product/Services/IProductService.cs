using TinyCRM.API.Modules.Contact.DTOs;
using TinyCRM.API.Modules.Product.DTOs;

namespace TinyCRM.API.Modules.Product.Services
{
    public interface IProductService
    {
        Task<GetProductDTO> AddAsync(AddOrUpdateProductDTO dto);
        Task DeleteAsync(Guid id);
        Task<IList<GetProductDTO>> GetAllAsync(int? skip, int? take, string? name, string? sortBy, bool? descending);
        Task<GetProductDTO> GetByIdAsync(Guid id);
        Task<GetProductDTO> UpdateAsync(AddOrUpdateProductDTO dto, Guid id);
    }
}