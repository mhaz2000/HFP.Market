using HFP.Shared.Models;

namespace HFP.Application.Queries.Transactions
{
    public record TransactionFilter : PaginationQuery
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
