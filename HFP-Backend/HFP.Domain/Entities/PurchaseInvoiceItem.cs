using HFP.Domain.ValueObjects.Products;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class PurchaseInvoiceItem : Entity<Guid>
    {
        public ProductName Name { get; private set; }
        public ProductQuantity Quantity { get; private set; }
        public ProductPrice Price { get; private set; }

        public Guid PurchaseInvoiceId { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; }

        public PurchaseInvoiceItem() { }
        public PurchaseInvoiceItem(ProductName name, ProductQuantity quantity, ProductPrice price, PurchaseInvoice purchaseInvoice)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            PurchaseInvoice = purchaseInvoice;
            PurchaseInvoiceId = purchaseInvoice.Id;
        }

        internal void Update(ProductName name, ProductQuantity quantity, ProductPrice price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
