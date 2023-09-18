namespace BuildingBlock.Application.DTOs;

public class FilterAndPagingResultDto<TDto>
{
    public FilterAndPagingResultDto(List<TDto> data, int page, int take, int totalCount)
    {
        Data = data;
        Meta = new MetaDto(page, take, totalCount);
    }

    public MetaDto Meta { get; private set; }
    public List<TDto> Data { get; private set; }
}

public class MetaDto
{
    public MetaDto(int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
    }

    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }
    public int PageSize { get; }
}