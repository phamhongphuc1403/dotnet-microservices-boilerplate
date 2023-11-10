namespace ProductManagement.Core.Application.DTOs;

public class DeleteManyProductsDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}