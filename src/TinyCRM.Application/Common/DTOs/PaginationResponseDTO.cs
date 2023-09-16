using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Common.DTOs;

public class PaginationResponseDto<T> where T : GuidBaseEntity
{
    public PaginationResponseDto()
    {
        Data = new List<T>();
        Meta = new MetaDto();
    }

    public PaginationResponseDto(List<T> data, int? page, int? take, int totalCount)
    {
        Data = data;
        Meta.CurrentPage = page ?? 1;
        Meta.PageSize = take ?? 10;
        Meta.TotalCount = totalCount;
        Meta.TotalPages = (int)Math.Ceiling(totalCount / (double)Meta.PageSize);
    }

    public MetaDto Meta { get; set; } = new();
    public List<T> Data { get; set; }
}

public class MetaDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
}