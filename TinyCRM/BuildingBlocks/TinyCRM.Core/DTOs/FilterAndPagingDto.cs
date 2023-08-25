using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace TinyCRM.Core.DTOs;

public abstract class FilterAndPagingDto<T>
{
    [Range(1, int.MaxValue, ErrorMessage = "The value must be larger than 0.")]
    public int? Take { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "The value must be larger than 0.")]
    public int? Page { get; set; }

    public string? Name { get; set; }
    
    public bool? IsDescending { get; set; }

    public abstract Expression<Func<T, bool>> BuildSearchExpression();

    public abstract string? BuildSort();
}