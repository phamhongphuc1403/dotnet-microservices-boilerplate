using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlock.Domain.Shared.Constants;

namespace BuildingBlock.Application.DTOs;

public class FilterAndPagingDto<TEnum>
{
    private TEnum? _sortBy;

    [StringLength(100, ErrorMessage = "Keyword cannot exceed 100 characters.")]
    public string Keyword { get; set; } = DefaultPaginationParameters.Keyword;

    [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be a non-negative number.")]
    public int PageIndex { get; set; } = DefaultPaginationParameters.PageIndex;

    [Range(1, int.MaxValue, ErrorMessage = "PageSize must be a positive number.")]
    public int PageSize { get; set; } = DefaultPaginationParameters.PageSize;

    public bool IsDescending { get; set; } = DefaultPaginationParameters.IsDescending;

    public TEnum? SortBy
    {
        get => _sortBy;
        set
        {
            _sortBy = value;
            Sort = ConvertSort();
        }
    }

    [JsonIgnore] public string Sort { get; private set; } = string.Empty;

    private string ConvertSort()
    {
        if (SortBy == null) return string.Empty;

        var sort = SortBy.ToString();

        sort = IsDescending ? $"{sort} desc" : $"{sort} asc";

        return sort;
    }
}