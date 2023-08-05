using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace TinyCRM.Application.Common.DTOs
{
    public abstract class DataQueryDTO<T>
    {
        [Range(1, int.MaxValue, ErrorMessage = "The value must be larger than 0.")]
        public int? Take { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value must be larger than 0.")]
        public int? Page { get; set; }

        public string? Name { get; set; }
        public bool? Descending { get; set; }

        public abstract Expression<Func<T, bool>> BuildFilterExpression();

        public abstract string BuildSort();
    }
}