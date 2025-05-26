using HFP.Domain.Entities;

namespace HFP.Infrastructure.EF.Models
{
    internal class PurchaseInvoiceItemReadModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public PurchaseInvoiceReadModel PurchaseInvoice { get; set; }
        public Guid PurchaseInvoiceId { get; set; }
    }
}
