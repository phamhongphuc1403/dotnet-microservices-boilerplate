using System.ComponentModel.DataAnnotations;
using BuildingBlock.Domain.Shared.Constants;

namespace BuildingBlock.Application.DTOs;

public class FilterAndPagingDto<TEnum>
{
    protected FilterAndPagingDto()
    {
    }

    protected FilterAndPagingDto(FilterAndPagingDto<TEnum> dto)
    {
        Keyword = dto.Keyword;
        PageSize = dto.PageSize;
        PageIndex = dto.PageIndex;
    }

    [StringLength(100, ErrorMessage = "Keyword cannot exceed 100 characters.")]
    public string Keyword { get; set; } = DefaultPaginationParameters.Keyword;

    [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be a non-negative number.")]
    public int PageIndex { get; set; } = DefaultPaginationParameters.PageIndex;

    [Range(1, int.MaxValue, ErrorMessage = "PageSize must be a positive number.")]
    public int PageSize { get; set; } = DefaultPaginationParameters.PageSize;

    public bool IsDescending { get; set; } = DefaultPaginationParameters.IsDescending;

    public virtual TEnum? SortBy { get; set; }

    public string ConvertSort()
    {
        if (SortBy == null) return string.Empty;

        var sort = SortBy.ToString();

        sort = IsDescending ? $"{sort} desc" : $"{sort} asc";

        return sort;
    }
}