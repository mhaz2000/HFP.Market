using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Transactions
{
    public record GetProfitReportQuery : PaginationQuery, IQuery<PaginatedResult<ProfitReportDto>>;
}
