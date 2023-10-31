namespace TinyCRM.Products.Application.DTOs;

public class DeleteManyProductsDto
{
    public DeleteManyProductsDto()
    {
    }

    protected DeleteManyProductsDto(DeleteManyProductsDto dto)
    {
        Ids = dto.Ids;
    }

    public IEnumerable<Guid> Ids { get; set; } = null!;
}