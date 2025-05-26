using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Products;
using HFP.Domain.ValueObjects.PurchaseInvoices;

namespace HFP.Domain.Factories.interfaces
{
    public interface IPurchaseInvoiceFactory
    {
        PurchaseInvoice Create(Guid? imageId, PurchaseInvoiceDate date);
        PurchaseInvoiceItem Create(ProductName name, ProductQuantity quantity, ProductPrice price, PurchaseInvoice purchaseInvoice);

        void Update(Guid? imageId, PurchaseInvoiceDate date, PurchaseInvoice purchaseInvoice);
        void Update(ProductName name, ProductQuantity quantity, ProductPrice price, PurchaseInvoiceItem purchaseInvoiceItem);

    }
}
