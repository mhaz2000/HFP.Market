using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.PurchaseInvoices
{
    public record GetPurchaseInvoicesQuery() : PaginationQuery, IQuery<PaginatedResult<PurchaseInvoiceDto>>;
}
