using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.Products;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.ModuleExtensions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetProductsHandler : IQueryHandler<GetProductsQuery, PaginatedResult<ProductDto>>
    {
        private readonly DbSet<ProductReadModel> _products;
        private readonly IMapper _mapper;

        public GetProductsHandler(ReadDbContext context, IMapper mapper)
        {
            _products = context.Products;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _products.AsQueryable();
            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{query.Search}%"));
            var products = dbQuery.AsNoTracking();
            var paginatedResult = await products.ToPaginatedResultAsync<ProductReadModel, ProductDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
