using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.PurchaseInvoices
{
    public record GetPurchaseInvoiceQuery(Guid Id) : IQuery<EditPurchaseInvoiceDto>;
}
