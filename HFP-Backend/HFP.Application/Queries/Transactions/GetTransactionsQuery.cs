using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Transactions
{
    public record GetTransactionsQuery : PaginationQuery, IQuery<PaginatedResult<TransactionDto>>;
}
