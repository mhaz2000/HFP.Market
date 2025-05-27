using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;

namespace HFP.Infrastructure.EF.Repositories
{
    internal sealed class PurchaseInvoiceRepository : GenericRepository<PurchaseInvoice>, IPurchaseInvoiceRepository
    {
        public PurchaseInvoiceRepository(WriteDbContext context) : base(context)
        {
        }

        public async Task AddItemAsync(PurchaseInvoiceItem purchaseInvoiceItem)
        {
            await _context.PurchaseInvoiceItems.AddAsync(purchaseInvoiceItem);
        }

        public void DeleteItem(PurchaseInvoiceItem purchaseInvoiceItem)
        {
            _context.PurchaseInvoiceItems.Remove(purchaseInvoiceItem);
        }
    }
}
