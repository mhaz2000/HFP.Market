using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.Transactions
{
    public record GetInvoiceQuery(string BuyerId) : IQuery<IEnumerable<ProductTransactionDto>>;
}
