using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Domain.Consts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.ModuleExtensions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetTransactionsHandler : IQueryHandler<GetTransactionsQuery, PaginatedResult<TransactionDto>>
    {
        private readonly DbSet<TransactionReadModel> _transactions;
        private readonly IMapper _mapper;
        public GetTransactionsHandler(ReadDbContext context, IMapper mapper)
        {
            _transactions = context.Transactions;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<TransactionDto>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _transactions.Include(t => t.ProductTransactions).ThenInclude(t => t.Product)
                .Where(c => !c.IsDeleted && c.Type == TransactionType.Invoice);

            var paginatedResult = await dbQuery.AsNoTracking()
                .ToPaginatedResultAsync<TransactionReadModel, TransactionDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;

        }
    }
}
