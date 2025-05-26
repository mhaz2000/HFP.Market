using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using HFP.Application.Commands.Transaction;
using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Domain.Consts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.ModuleExtensions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Helpers;
using HFP.Shared.Models;
using Microsoft.EntityFrameworkCore;

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
                .Where(c => !c.IsDeleted && c.Type == TransactionType.Invoice && c.ProductTransactions.Any());

            if(!string.IsNullOrEmpty(query.StartDate) && !string.IsNullOrEmpty(query.EndDate))
            {
                var startDate = query.StartDate.ToDate(true);
                var endDate = query.EndDate.ToDate(false);

                dbQuery = dbQuery.Where(t => t.Date.Date <= endDate && t.Date.Date >= startDate);
            }

            var paginatedResult = await dbQuery.AsNoTracking()
                .ToPaginatedResultAsync<TransactionReadModel, TransactionDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;

        }
    }
}
