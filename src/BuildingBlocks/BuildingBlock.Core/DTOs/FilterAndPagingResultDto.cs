using BuildingBlock.Core.Constants;

namespace BuildingBlock.Core.DTOs;

public class FilterAndPagingResultDto<TEntity> where TEntity : GuidBaseEntity
{
    public MetaDto Meta { get; private set; }
    public List<TEntity> Data { get; private set; }

    public FilterAndPagingResultDto(List<TEntity> data, int? page, int? take, int totalCount)
    {
        Data = data;
        Meta = new MetaDto(page, take, totalCount);
    }
}

public class MetaDto
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }
    public int PageSize { get; private set; }

    public MetaDto(int? page, int? take, int totalCount)
    {
        CurrentPage = page ?? Pagination.Page;
        PageSize = take ?? Pagination.PageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
    }
}