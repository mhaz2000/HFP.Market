using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.Transactions
{
    public record GetTransactionQuery(Guid id) : IQuery<IEnumerable<ProductTransactionDto>>;
}
