using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.PaginationHelper
{
    public class PaginationResponse<T> where T : GuidBaseEntity
    {
        public MetaDTO Meta { get; set; } = new();
        public List<T> Data { get; set; }

        public PaginationResponse()
        {
            Data = new List<T>();
            Meta = new MetaDTO();
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

    public class MetaDTO
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}