using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.PaginationHelper
{
    public class PaginationResponse<T> where T : GuidBaseEntity
    {
        public MetaDto Meta { get; set; } = new();
        public List<T> Data { get; set; }

        public PaginationResponse()
        {
            Data = new List<T>();
            Meta = new MetaDto();
        }

        public PaginationResponse(List<T> data, int? page, int? take, int totalCount)
        {
            Data = data;
            Meta.CurrentPage = page ?? 1;
            Meta.PageSize = take ?? 10;
            Meta.TotalCount = totalCount;
            Meta.TotalPages = (int)Math.Ceiling(totalCount / (double)Meta.PageSize);
        }
    }

    public class MetaDto
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}