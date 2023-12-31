namespace BuildingBlock.Core.Application.DTOs;

public class FilterAndPagingResultDto<TDto>
{
    public FilterAndPagingResultDto(IEnumerable<TDto> data, int pageIndex, int pageSize, int totalCount)
    {
        Data = data;
        Meta = new MetaDto(pageIndex, pageSize, totalCount);
    }

    public MetaDto Meta { get; }
    public IEnumerable<TDto> Data { get; }
}

public class MetaDto
{
    public MetaDto(int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int PageSize { get; }
}