using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.Discounts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.ModuleExtensions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetDiscountsHandler : IQueryHandler<GetDiscountsQuery, PaginatedResult<DiscountDto>>
    {
        private readonly DbSet<DiscountReadModel> _discount;
        private readonly IMapper _mapper;

        public GetDiscountsHandler(ReadDbContext context, IMapper mapper)
        {
            _discount = context.Discounts;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<DiscountDto>> Handle(GetDiscountsQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _discount.AsQueryable();
            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{query.Search}%") ||
                    Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Code, $"%{query.Search}%"));

            var paginatedResult = await dbQuery.AsNoTracking()
                .ToPaginatedResultAsync<DiscountReadModel, DiscountDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
