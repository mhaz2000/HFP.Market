using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Transactions
{
    public record GetProfitReportExcelQuery : PaginationQuery, IQuery<MemoryStream>;
}
