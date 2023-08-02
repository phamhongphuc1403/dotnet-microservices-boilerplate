using System.Linq.Expressions;
using TinyCRM.API.Common.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Params;

namespace TinyCRM.API.Utilities
{
    public class PaginationBuilder<T> where T : GuidBaseEntity
    {
        private readonly DataQueryDTO<T> _query;
        private readonly List<string> _includes;
        private readonly List<Expression<Func<T, bool>>> _expressionList = new();

        private PaginationBuilder(DataQueryDTO<T> query)
        {
            _query = query;
            _includes = new List<string>();
            _expressionList.Add(_query.BuildFilterExpression());
        }

        public static PaginationBuilder<T> Init(DataQueryDTO<T> query)
        {
            return new PaginationBuilder<T>(query);
        }

        public PaginationBuilder<T> JoinTable(string table)
        {
            _includes.Add(table);
            return this;
        }

        public PaginationBuilder<T> AddConstraint(Expression<Func<T, bool>> expression)
        {
            _expressionList.Add(expression);

            return this;
        }

        public PaginationParams<T> Build()
        {
            var sortBy = _query.BuildSort();

            if (!string.IsNullOrEmpty(sortBy))
            {
                sortBy += _query.Descending == true ? " desc" : "";
            }

            return new PaginationParams<T>
            {
                Take = _query.Take ?? 10,
                Skip = (_query.Page - 1 ?? 0) * (_query.Take ?? 10),
                ExpressionList = _expressionList,
                Includes = _includes,
                SortBy = sortBy
            };
        }
    }
}