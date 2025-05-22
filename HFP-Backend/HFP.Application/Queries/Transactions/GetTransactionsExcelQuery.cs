using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Transactions
{
    public record GetTransactionsExcelQuery : PaginationQuery, IQuery<MemoryStream>;
}
