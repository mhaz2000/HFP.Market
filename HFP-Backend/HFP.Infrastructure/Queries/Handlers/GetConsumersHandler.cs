using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.Consumers;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.ModuleExtensions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetConsumersHandler : IQueryHandler<GetConsumersQuery, PaginatedResult<ConsumerDto>>
    {
        private readonly DbSet<ConsumerReadModel> _consumers;
        private readonly IMapper _mapper;

        public GetConsumersHandler(ReadDbContext context, IMapper mapper)
        {
            _consumers = context.Consumers;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ConsumerDto>> Handle(GetConsumersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _consumers.AsQueryable();
            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{query.Search}%"));
            var consumers = dbQuery.AsNoTracking();
            var paginatedResult = await consumers.ToPaginatedResultAsync<ConsumerReadModel, ConsumerDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
