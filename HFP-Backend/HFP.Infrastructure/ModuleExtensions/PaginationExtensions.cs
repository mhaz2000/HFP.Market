using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HFP.Shared.Models;
using System.Globalization;
using System.Linq.Dynamic.Core;

namespace HFP.Infrastructure.ModuleExtensions
{
    public static class PaginationExtensions
    {
        public static async Task<PaginatedResult<TResult>> ToPaginatedResultAsync<TQuery, TResult>(
            this IQueryable<TQuery> source,
            int pageIndex,
            int pageSize, string sortBy, IMapper mapper)
        {

            if (!string.IsNullOrEmpty(sortBy))
            {
                var sortParts = sortBy.Split('~');
                if (sortParts.Length == 2)
                {
                    var field = sortParts[0];
                    var direction = sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";
                    source = source.OrderBy($"{field} {direction}");
                }
            }

            var totalCount = await source.CountAsync();
            var items = await source
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return new PaginatedResult<TResult>(mapper.Map<IEnumerable<TResult>>(items), totalCount, pageSize, pageIndex);
        }

        public static async Task<PaginatedResult<TResult>> ToPaginatedResultAsync<TResult>(
            this IQueryable<TResult> source,
            int pageIndex,
            int pageSize, string sortBy)
        {

            if (!string.IsNullOrEmpty(sortBy))
            {
                var sortParts = sortBy.Split('~');
                if (sortParts.Length == 2)
                {
                    var field = sortParts[0];
                    var direction = sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";
                    source = source.OrderBy($"{field} {direction}");
                }
            }

            var totalCount = await source.CountAsync();
            var items = await source
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return new PaginatedResult<TResult>(items, totalCount, pageSize, pageIndex);
        }
    }
}
