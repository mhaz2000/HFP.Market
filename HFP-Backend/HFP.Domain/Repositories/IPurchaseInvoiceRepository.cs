using HFP.Domain.Entities;
using HFP.Domain.Repositories.Base;

namespace HFP.Domain.Repositories
{
    public interface IPurchaseInvoiceRepository : IGenericRepository<PurchaseInvoice>
    {
        Task AddItemAsync(PurchaseInvoiceItem purchaseInvoiceItem);
    }
}
