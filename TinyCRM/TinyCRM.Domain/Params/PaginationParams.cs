using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Params
{
    public class PaginationParams<T> where T : GuidBaseEntity
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public List<Expression<Func<T, bool>>> ExpressionList { get; set; } = null!;
        public List<string> Includes { get; set; } = new();
        public string SortBy { get; set; } = null!;
    }
}